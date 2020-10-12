using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SSS
{
	#region Enums
	public enum RuneState { OFF, FLYING, CHARGING, ATTACK };
	#endregion

	public class Rune : MonoBehaviour, ISwipeable, IThrowable, IPointerEnterHandler
	{
#pragma warning disable 0649

		public static int _thisLevelsRuneDmg = 10;
		public static int _thisLevelsPlayerDmg = 10;

		#region References
		#region public
		public static List<Rune> _runes = new List<Rune>();
		public static List<Rune> _activeRunes
		{
			get
			{
				List<Rune> offRunes = new List<Rune>();

				foreach (var item in _runes)
				{
					if (!item.gameObject.activeSelf)
						offRunes.Add(item);
				}

				foreach (var item in offRunes)
				{
					_runes.Remove(item);
				}

				return _runes;
			}
		}
		public Image _image;
		public Image _chargedImage;
		#endregion

		#region private
		private InputManager _inputManager;
		private SpawnParticleOnDeactivation _spawnParticleOnDeactivation;
		private Propeller _myPropeller;
		#endregion
		#endregion

		#region Variables
		#region public
		public bool _isPlayerOwned = true;
		public ThrowSide _entrySide = ThrowSide.BOTTOM;
		public float _flySpeed = 1f;
		#endregion

		#region private
		private RuneState _state = RuneState.OFF;

		[Header("Flight")]
		private Vector3[] _path = new Vector3[0];//spline points
		private float _t = 0f;
		[SerializeField] [Range(.5f, 10f)] [Tooltip("How fast does the rune fly after charging up?")] private float _attackFlySpeed = 1.5f;

		[Header("Charging")]
		private float _chargeLevel = 0;
		[SerializeField] [Range(0.1f, 2f)] private float _chargeMaxTime = 1f;

		[Header("Damage")]
		[SerializeField] [Range(1, 100)] private int _damageToPlayer;
		[SerializeField] [Range(1, 100)] private int _damageToEnemy;
		#endregion
		#endregion

		#region MonoBehaviour
		void Awake()
		{
			_image = GetComponent<Image>();
			_chargedImage = transform.GetChild(0).GetComponent<Image>();
			_inputManager = FindObjectOfType<InputManager>();
			_spawnParticleOnDeactivation = GetComponent<SpawnParticleOnDeactivation>();
			_myPropeller = GetComponent<Propeller>();
		}

		void Start()
		{

		}

		void Update()
		{

		}

		void FixedUpdate()
		{
			FixedUpdateState();
		}

		void OnEnable()
		{
			if (!_runes.Contains(this))
				_runes.Add(this);

			SetState(RuneState.FLYING);
			SetChargedAlpha(0f);
		}

		void OnDisable()
		{
			if (_runes.Contains(this))
				_runes.Remove(this);

			SetState(RuneState.OFF);
		}

		void OnDestroy()
		{

		}
		#endregion

		#region ISwipeable
		public bool _isSwipeable { get; set; }

		public void Activate() => _isSwipeable = true;
		public void Swipe() => SetState(RuneState.CHARGING);
		public void Deactivate() => _isSwipeable = false;
		#endregion

		#region IThrowable
		public void StartThrow()
		{
			_t = 0f;
			CalculateNewPath();
			SetState(RuneState.FLYING);
		}

		public void ContinueFly()
		{
			if (_state == RuneState.ATTACK)
			{
				transform.position += _attackFlySpeed * Time.fixedDeltaTime * (_isPlayerOwned ? Vector3.up : Vector3.down);
				return;
			}

			if (_state == RuneState.FLYING)
			{
				if (_path.Length < 4)
					CalculateNewPath();

				transform.position = Bezier.GetPoint(_path, _t);
				_t += Time.fixedDeltaTime * _flySpeed;

				if (1f < _t)
				{
					if (_isPlayerOwned)
					{
						MatchController.s_instance.PlayerTakesDamage(_damageToPlayer);
						SetState(RuneState.OFF);
						gameObject.SetActive(false);
					}
					else
						SetState(RuneState.CHARGING);
				}
			}
		}

		public void CalculateNewPath()
		{
			/*
			if (_isPlayerOwned)
			{
				Vector3 p0 = MatchController.s_instance.RandomBottomPoint();
				Vector3 p3 = MatchController.s_instance.RandomBottomPoint();

				Vector3 p1 = new Vector3(Mathf.Lerp(p0.x, p3.x, UnityEngine.Random.value), MatchController.s_instance._panelCorners[1].y, p0.z);
				Vector3 p2 = new Vector3(Mathf.Lerp(p1.x, p3.x, UnityEngine.Random.value), MatchController.s_instance._panelCorners[1].y, p0.z);

				_path = new Vector3[4] { p0, p1, p2, p3 };
			}
			else
			{
				Vector3 p0 = MatchController.s_instance.RandomTopPoint();
				Vector3 p3 = MatchController.s_instance.RandomMiddlePoint();

				Vector3 p1 = new Vector3(Mathf.Lerp(p0.x, p3.x, UnityEngine.Random.value), Mathf.Lerp(p0.y, p3.y, UnityEngine.Random.value), p0.z);
				Vector3 p2 = new Vector3(Mathf.Lerp(p1.x, p3.x, UnityEngine.Random.value), Mathf.Lerp(p1.y, p3.y, UnityEngine.Random.value), p0.z);

				_path = new Vector3[4] { p0, p1, p2, p3 };
			}
			*/
			if (_entrySide != ThrowSide.SETPATH)
				_path = PathFactory.MakePath(_entrySide);
		}

		public void SetPath(Vector3[] newPath) => _path = newPath;
		#endregion

		#region State Machine
		public void SetState(RuneState newState)
		{
			switch (newState)
			{
				case RuneState.OFF:
					Deactivate();
					break;
				case RuneState.FLYING:
					Activate();
					break;
				case RuneState.CHARGING:
					_chargeLevel = 0f;
					_myPropeller.StartPullForward();
					if (_isPlayerOwned)
						Deactivate();
					break;
				case RuneState.ATTACK:
					Deactivate();
					break;
				default:
					return;
			}

			_state = newState;
		}

		public void FixedUpdateState()
		{
			switch (_state)
			{
				case RuneState.OFF:
					break;
				case RuneState.FLYING:
					ContinueFly();
					break;
				case RuneState.CHARGING:
					ChargeTick();
					_myPropeller.PullForward(_chargeLevel / _chargeMaxTime);
					break;
				case RuneState.ATTACK:
					ContinueFly();
					CheckAttack();
					break;
				default:
					break;
			}
		}
		#endregion

		#region Charge and Attack
		private void ChargeTick()
		{
			_chargeLevel += Time.fixedDeltaTime;
			SetChargedAlpha(_chargeLevel / _chargeMaxTime);

			if (_chargeMaxTime < _chargeLevel)
				SetState(RuneState.ATTACK);
		}

		private void CheckAttack()
		{
			if (_isPlayerOwned)
			{
				if (MatchController.s_instance.IsOverTop(transform.position))
				{
					if (!BlockController.s_instance.WasIBlocked(transform.position))
						MatchController.s_instance.EnemyTakesDamage(_damageToEnemy);
					else
						GotBlocked();

					SetState(RuneState.OFF);
					gameObject.SetActive(false);
				}
			}
			else if (MatchController.s_instance.IsUnderBot(transform.position))
			{
				MatchController.s_instance.PlayerTakesDamage(_damageToPlayer);
				SetState(RuneState.OFF);
				gameObject.SetActive(false);
			}
		}

		private void GotBlocked()
		{
			ParticleFactory.s_spawnParticle?.Invoke(ParticleType.RUNE, transform.position);
			MatchController.s_instance._enemyAnimationController.PlayAnimation(MatchController.s_instance._enemyAnimationController._blockAnim, false, 1f);
		}
		#endregion

		#region IPointerEnterHandler
		public void OnPointerEnter(PointerEventData pointerEventData)
		{
			if (_isSwipeable && _inputManager._canSlice)
			{
				//Debug.Log("Cursor Entering ");
				if (null == _inputManager)
					_inputManager = FindObjectOfType<InputManager>();

				if (null != _inputManager && null != _inputManager._slice)
					_inputManager._slice.SetActive(true);
				_image.raycastTarget = false;
				StartCoroutine(ESliceRune(0.1f));
			}
		}

		IEnumerator ESliceRune(float destroyTimer)
		{
			yield return new WaitForSeconds(destroyTimer);
			if (_isPlayerOwned)
			{
				SetState(RuneState.CHARGING);
				BlockController.s_instance.IncreaseCharge();
			}
			else
				this.gameObject.SetActive(false);

			//Debug.Log("Cursor exit");
			//_inputManager = FindObjectOfType<InputManager>();
			//_inputManager._slice.SetActive(false);
		}
		#endregion

		private void SetChargedAlpha(float alpha)
		{
			var tmp = _chargedImage.color;
			tmp.a = alpha;
			_chargedImage.color = tmp;
		}

		public void ResetValues()
		{
			_t = 0f;
			_damageToEnemy = Rune._thisLevelsRuneDmg;
			_damageToPlayer = Rune._thisLevelsPlayerDmg;
			StartThrow();
			_image.raycastTarget = true;
		}

		public void SetParticle(ParticleType type) => _spawnParticleOnDeactivation._particleType = type;
	}
}
