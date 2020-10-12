using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SSS
{
	#region Enums
	#endregion

	public class MatchController : Singleton<MatchController>
	{
#pragma warning disable 0649

		#region References
		#region public
		[SerializeField] public RectTransform _panel;
		#endregion

		#region private
		[SerializeField] private Slider _enemyHealthSlider;
		[SerializeField] private Slider _playerHealthSlider;
		[SerializeField] private Slider _playerProvisionalSlider;
		[SerializeField] private Slider _enemyBlockSlider;
		[SerializeField] private TMPro.TextMeshProUGUI _levelText;
		#endregion
		#endregion

		#region Variables
		#region public
		public bool _isMatchRunning = true;
		public Vector3[] _panelCorners = new Vector3[4];//clockwise, starting bottom left
		public int _playerHealth = 100;
		public int _playerMaxHealth = 100;
		public int _playerProvisionalHealth = 100;
		public int _enemyHealth = 100;
		public int _enemyMaxHealth = 100;

		public int _minRunes, _maxRunes, _minShuriken, _maxShuriken;
		public float _minTime, _maxTime;
		public EnemyAnimationController _enemyAnimationController;
		private RectTransform _enemySpotBackingfield;
		public RectTransform _enemySpot
		{
			get
			{
				if (null == _enemySpotBackingfield)
				{
					_enemySpotBackingfield = FindObjectOfType<EnemySpot>()?.GetComponent<RectTransform>();
					if (null == _enemySpotBackingfield)
						return new RectTransform();
					else
						return _enemySpotBackingfield;
				}
				else
					return _enemySpotBackingfield;
			}
			set => _enemySpotBackingfield = value;
		}
		#endregion

		#region private
		#endregion
		private float _regenBuffer = 0f;
		[SerializeField] private float _lifeRegenPerSecond = 1f;
		#endregion

		#region MonoBehaviour
		void Start()
		{
			SceneLoader.s_instance.LoadLevel(GameController.s_instance._whichLevel);
			if (null == _enemyAnimationController)
				FindObjectOfType<EnemyAnimationController>()?.Initialise();

			ContinueGame();
			_panel.GetWorldCorners(_panelCorners);
			//StartCoroutine("ThrowRoutine");
			//StartCoroutine("RandomRoutine");
			//StartCoroutine("PatternedThrowRoutine");
			StartCoroutine(GDThrowRoutine());

			_regenBuffer = 0f;

			_levelText.text = GameController.s_instance._whichLevel.ToString();
		}

		void Update()
		{
			Regeneration();

			_enemyHealthSlider.value = _enemyHealth;
			_playerHealthSlider.value = _playerHealth;
			_playerProvisionalSlider.value = Mathf.Abs(_playerProvisionalSlider.value - _playerProvisionalHealth) < 1f ? _playerProvisionalHealth : Mathf.Lerp(_playerProvisionalSlider.value, _playerProvisionalHealth, 0.01f);

			//_levelText.text = GameController.s_instance._whichLevel.ToString();
		}

		void OnEnable()
		{

		}

		void OnDisable()
		{

		}

		void OnDestroy()
		{

		}
		#endregion

		#region Health and Damage
		public void ResetHealth()
		{
			_playerHealth = _playerMaxHealth;
			_playerProvisionalHealth = _playerMaxHealth;
			_enemyHealth = _enemyMaxHealth;
		}

		public void EnemyTakesDamage(int amount)
		{
			if (amount < 0)
				return;

			_enemyHealth -= amount;
			_enemyAnimationController?.PlayAnimation(_enemyAnimationController._gettingHitAnim, false, 1f);

			if (_enemyHealth <= 0)//check for win
			{
				PlayerWins();
				return;
			}
		}

		public void PlayerTakesDamage(int amount)
		{
			if (amount < 0)
				return;

			_playerHealth -= amount;
			_playerProvisionalHealth = _playerHealth;

			if (_playerHealth <= 0)//check for loss
			{
				PlayerLoses();
				return;
			}
		}

		public void ProvisionalDamage(int amount)
		{
			_playerHealth -= amount;

			if (_playerHealth <= 0)//check for loss
			{
				_playerProvisionalHealth = 0;
				PlayerLoses();
				return;
			}
		}

		private void Regeneration()
		{
			if (_playerProvisionalHealth <= _playerHealth)
				return;

			_regenBuffer += _lifeRegenPerSecond * Time.deltaTime;
			int overflow = (int)_regenBuffer;
			_regenBuffer -= overflow;
			_playerHealth += overflow;
		}
		#endregion

		#region EndGame
		private void PlayerWins()
		{
			if (!_isMatchRunning)
				return;

			_enemyAnimationController.PlayAnimation(_enemyAnimationController._deathAnim, false, 1f);
			GameEnd();
			Debug.Log("W U H U !! \\o/ enemy ded \\o/");
			ButtonController.s_instance.WinScreen();
			GameController.s_instance.LevelBeaten();
		}

		private void PlayerLoses()
		{
			if (!_isMatchRunning)
				return;

			GameEnd();
			//Debug.Log("O H  N O E S !! you got dedded X.X");
			ButtonController.s_instance.LoseScreen();
			GameController.s_instance.LoseLife();
		}

		private void GameEnd()
		{
			_isMatchRunning = false;
			_enemyAnimationController?.Yeet();
			PauseGame();
		}
		#endregion

		#region Panel
		public Vector3 RandomTopPoint() => Vector3.Lerp(_panelCorners[1], _panelCorners[2], Random.value);
		public Vector3 RandomBottomPoint() => Vector3.Lerp(_panelCorners[0], _panelCorners[3], Random.value);
		public Vector3 RandomMiddlePoint() => new Vector3(Mathf.Lerp(_panelCorners[1].x, _panelCorners[2].x, Random.value), Mathf.Lerp(_panelCorners[0].y, _panelCorners[1].y, 0.5f), _panelCorners[0].z);
		public Vector3 RandomLeftPoint() => Vector3.Lerp(_panelCorners[0], _panelCorners[1], Random.value);
		public Vector3 RandomRightPoint() => Vector3.Lerp(_panelCorners[2], _panelCorners[3], Random.value);

		public Vector3 RandomBottomLeftPoint() => Vector3.Lerp(_panelCorners[0], _panelCorners[3], Random.Range(0f, 0.7f));
		public Vector3 RandomBottomRightPoint() => Vector3.Lerp(_panelCorners[0], _panelCorners[3], Random.Range(0.3f, 1f));
		public Vector3 RandomTopLeftPoint() => Vector3.Lerp(_panelCorners[1], _panelCorners[2], Random.Range(0f, 0.7f));
		public Vector3 RandomTopRightPoint() => Vector3.Lerp(_panelCorners[1], _panelCorners[2], Random.Range(0.3f, 1f));

		public bool IsOverTop(Vector3 pos) => _panelCorners[1].y <= pos.y;
		public bool IsUnderBot(Vector3 pos) => pos.y <= _panelCorners[0].y;
		public bool IsInPanel(Vector3 pos) => _panelCorners[0].x <= pos.x && pos.x <= _panelCorners[3].x && _panelCorners[0].y <= pos.y && pos.y <= _panelCorners[1].y;

		/// <summary>
		/// Returns the X-Factor, meaning the horizontal percentile of the _xInput regarding the play area clamped to [0, 1].
		/// </summary>
		public float RelativeHorizontalPosition(float inputWorldX)
		{
			if (null == _panelCorners || _panelCorners.Length < 4)
			{
				Debug.Log("No panel corners, defaulting relative horizontal position to 0.5f.");
				return 0.5f;
			}

			float x0 = _panelCorners[1].x;
			float dif = _panelCorners[2].x - x0;

			if (0f < dif)
				return Mathf.Clamp(inputWorldX - x0 / dif, 0f, 1f);//effective line if everything checks out
			else
			{
				Debug.Log("Panel corner difference is < 0, defaulting relative horizontal position to 0.5f.");
				return 0.5f;
			}
		}

		public float ActualHorizontalPosition(float relativeX)
		{
			if (null == _panelCorners || _panelCorners.Length < 4)
			{
				Debug.Log("No panel corners, defaulting actual horizontal position to nonsense.");
				return 0.5f;
			}

			return Mathf.Lerp(_panelCorners[4].x, _panelCorners[3].x, relativeX);
		}
		#endregion

		#region Big Throws
		private void PlayerThrow()
		{
			int howManyRunes = (int)(5f * Random.value + 1f);

			for (int i = 0; i < howManyRunes; i++)
			{
				RuneFactory.s_instance.SpawnRune(RandomBottomPoint(), PathFactory.RandomThrowSide(), true);
			}
		}

		private void EnemyThrow()
		{
			for (int i = 0; i < 4; i++)
			{
				RuneFactory.s_instance.SpawnRune(RandomTopPoint(), ThrowSide.TOP, false);
			}
		}

		private void ShurikenThrow() => ShurikenFactory.s_instance.SpawnShuriken(RandomBottomPoint());

		private IEnumerator ThrowRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(2f);
				PlayerThrow();
				ShurikenThrow();
				yield return new WaitForSeconds(2f);
			}
		}
		#endregion

		private IEnumerator PatternedThrowRoutine()
		{
			yield return new WaitForSeconds(1f);

			while (true)
			{
				float randomTime = Random.Range(_minTime, _maxTime);
				RandomPatternedThrow(_minRunes, _maxRunes, _minShuriken, _maxShuriken, randomTime);

				yield return new WaitForSeconds(randomTime + 0.5f);
			}
		}

		private void RandomPatternedThrow(int minRunes, int maxRunes, int minShuriken, int maxShuriken, float totalTime)
		{
			int runes = Random.Range(minRunes, maxRunes + 1);
			float[] runeTimes = PathFactory.MakeTimings(PathFactory.RandomThrowTimingType(), totalTime, runes);
			//insert entryside patterns here

			for (int i = 0; i < runes; i++)
				StartCoroutine(DelayedRune(runeTimes[i], PathFactory.RandomThrowSide()));

			int shuriken = Random.Range(minShuriken, maxShuriken + 1);
			float[] shurikenTimes = PathFactory.MakeTimings(PathFactory.RandomThrowTimingType(), totalTime, shuriken);
			//insert entryside patterns here

			for (int i = 0; i < shuriken; i++)
				StartCoroutine(DelayedShuriken(shurikenTimes[i], ThrowSide.TOP));
		}

		#region Random Throws
		private IEnumerator RandomRoutine()
		{
			yield return new WaitForSeconds(1f);

			while (true)
			{
				float randomTime = Random.Range(_minTime, _maxTime);
				RandomThrow(_minRunes, _maxRunes, _minShuriken, _maxShuriken, randomTime);
				yield return new WaitForSeconds(randomTime);
				//RandomThrow(1, 6, 0, 4, 2.5f);
				//yield return new WaitForSeconds(2.5f);
			}
		}

		private void RandomThrow(int minRunes, int maxRunes, int minShuriken, int maxShuriken, float totalTime)
		{
			int runes = Random.Range(minRunes, maxRunes + 1);

			for (int i = 0; i < runes; i++)
				StartCoroutine("DelayedRune", totalTime * Random.value);

			int shuriken = Random.Range(minShuriken, maxShuriken + 1);

			for (int i = 0; i < shuriken; i++)
				StartCoroutine("DelayedShuriken", totalTime * Random.value);
		}

		private IEnumerator DelayedRune(float delay, ThrowSide entrySide = ThrowSide.BOTTOM)
		{
			yield return new WaitForSeconds(delay);
			RuneFactory.s_instance.SpawnRune(RandomBottomPoint(), entrySide, true);
		}

		private IEnumerator DelayedShuriken(float delay, ThrowSide entrySide = ThrowSide.TOP)
		{
			yield return new WaitForSeconds(delay);
			ShurikenFactory.s_instance.SpawnShuriken(RandomBottomPoint(), entrySide);
		}
		#endregion

		public void BlockSlider(float value)
		{
			_enemyBlockSlider.value = value;
		}

		static void PauseGame() => Time.timeScale = 0;
		static void ContinueGame() => Time.timeScale = 1;

		#region Throws with paths from game designer
		private IEnumerator GDThrowRoutine()
		{
			yield return new WaitForSeconds(1f);

			int levelNumber = Mathf.Max(0, GameController.s_instance._whichLevel - 1);
			if (PatternBase.s_instance._levels?.Length <= levelNumber)
			{
				Debug.Log("Found no patterns for this level.");
			}
			else
			{
				if (null != PatternBase.s_instance._levels[levelNumber]?._patterns)
				{
					PatternName[] patternNames = PatternBase.s_instance._levels[levelNumber]?._patterns;

					for (int i = 0; i < patternNames.Length; i++)
					{
						PatternBase.Pattern pat = PatternBase.s_instance.GetPattern(patternNames[i]);

						if (null != pat?._yeetables)
						{
							foreach (PatternBase.Yeetable yeet in pat._yeetables)
								StartCoroutine(yeet._type == RuneOrShuriken.RUNE ? DelayedPatternedRune(yeet, pat._totalTime) : DelayedPatternedShuriken(yeet, pat._totalTime));
						}

						yield return new WaitForSeconds(pat._totalTime + 0.5f);
					}
				}

				yield return new WaitForSeconds(3f);
			}

			PlayerLoses();
		}

		private void GDStraightThrow(PatternBase.Yeetable yeet)
		{
			/*
			int runes = Random.Range(minRunes, maxRunes + 1);
			float[] runeTimes = PathFactory.MakeTimings(PathFactory.RandomThrowTimingType(), totalTime, runes);
			//insert entryside patterns here

			for (int i = 0; i < runes; i++)
				StartCoroutine(DelayedRune(runeTimes[i], PathFactory.RandomThrowSide()));

			int shuriken = Random.Range(minShuriken, maxShuriken + 1);
			float[] shurikenTimes = PathFactory.MakeTimings(PathFactory.RandomThrowTimingType(), totalTime, shuriken);
			//insert entryside patterns here

			for (int i = 0; i < shuriken; i++)
				StartCoroutine(DelayedShuriken(shurikenTimes[i], ThrowSide.TOP));
				*/
		}

		private IEnumerator DelayedPatternedRune(PatternBase.Yeetable yeet, float totalTime)
		{
			yield return new WaitForSeconds(yeet._spawnTime * totalTime);
			RuneFactory.s_instance.SpawnRune(MakePathFromYeet(yeet), yeet._speed);
		}

		private IEnumerator DelayedPatternedShuriken(PatternBase.Yeetable yeet, float totalTime)
		{
			yield return new WaitForSeconds(yeet._spawnTime * totalTime);
			ShurikenFactory.s_instance.SpawnShuriken(MakePathFromYeet(yeet), yeet._speed);
		}

		private Vector3[] MakePathFromYeet(PatternBase.Yeetable yeet)
		{
			Vector3[] arr = new Vector3[4];

			arr[0] = GetEdgePoint(yeet._entrySide, yeet._entryPoint);
			arr[3] = GetEdgePoint(yeet._exitSide, yeet._exitPoint);
			arr[1] = Vector3.Lerp(arr[0], arr[3], 1f / 3f);
			arr[2] = Vector3.Lerp(arr[0], arr[3], 2f / 3f);

			return arr;
		}

		private Vector3 GetEdgePoint(ThrowSide side, float ratio)
		{
			switch (side)
			{
				case ThrowSide.BOTTOM:
					return Vector3.Lerp(_panelCorners[0], _panelCorners[3], ratio);
				case ThrowSide.LEFT:
					return Vector3.Lerp(_panelCorners[0], _panelCorners[1], ratio);
				case ThrowSide.RIGHT:
					return Vector3.Lerp(_panelCorners[3], _panelCorners[2], ratio);
				case ThrowSide.TOP:
					return Vector3.Lerp(_panelCorners[1], _panelCorners[2], ratio);
				case ThrowSide.SETPATH:
				default:
					return Vector3.Lerp(_panelCorners[0], _panelCorners[3], 0.5f);
			}
		}
		#endregion
	}
}
