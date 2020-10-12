using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SSS
{
	#region Enums
	#endregion

	public class Shuriken : MonoBehaviour, IThrowable, IPointerEnterHandler
	{
		public static int _thisLevelsProvDamage = 10;

		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		public ThrowSide _entrySide = ThrowSide.TOP;
		public bool _touchBlock;
		public float _flySpeed = 1f;
		#endregion

		#region private
		[SerializeField] private int _provisionalDamage = 10;
		[Header("Flight")]
		private Vector3[] _path = new Vector3[0];//spline points
		private float _t = 0f;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{

		}

		void Update()
		{

		}

		void FixedUpdate()
		{
			ContinueFly();
		}

		void OnEnable()
		{
			_touchBlock = false;
		}

		void OnDisable()
		{

		}

		void OnDestroy()
		{

		}
		#endregion

		#region IThrowable
		public void StartThrow()
		{
			_t = 0f;
			CalculateNewPath();
		}

		public void ContinueFly()
		{
			if (_path.Length < 4)
				CalculateNewPath();

			transform.position = Bezier.GetPoint(_path, _t);
			_t += Time.fixedDeltaTime * _flySpeed;

			if (1f < _t)
				gameObject.SetActive(false);
		}

		public void CalculateNewPath()
		{
			/*
			Vector3 p0 = MatchController.s_instance.RandomBottomPoint();
			Vector3 p3 = MatchController.s_instance.RandomBottomPoint();

			Vector3 p1 = new Vector3(Mathf.Lerp(p0.x, p3.x, UnityEngine.Random.value), MatchController.s_instance._panelCorners[1].y, p0.z);
			Vector3 p2 = new Vector3(Mathf.Lerp(p1.x, p3.x, UnityEngine.Random.value), MatchController.s_instance._panelCorners[1].y, p0.z);

			_path = new Vector3[4] { p0, p1, p2, p3 };
			*/

			if (_entrySide != ThrowSide.SETPATH)
				_path = PathFactory.MakePath(_entrySide);
		}

		public void SetPath(Vector3[] newPath) => _path = newPath;
		#endregion

		#region IPointerEnterHandler
		public void OnPointerEnter(PointerEventData pointerEventData)
		{
			if (!_touchBlock)
			{
				_touchBlock = true;
				MatchController.s_instance.ProvisionalDamage(_provisionalDamage);
				ButtonController.s_instance.Vignette();
				AutoFade.s_resetFade.Invoke(0);
				gameObject.SetActive(false);
			}
		}
		#endregion

		public void ResetValues()
		{
			_touchBlock = false;
			_t = 0f;
			_provisionalDamage = _thisLevelsProvDamage;
			StartThrow();
		}
	}
}