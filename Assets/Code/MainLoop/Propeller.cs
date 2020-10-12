using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace SSS
{
	#region Enums
	#endregion

	public class Propeller : MonoBehaviour
	{
		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		#endregion

		#region private
		private bool _isRotating;
		[SerializeField] [Range(0, 5)] private float _minRotation = 0f;
		[SerializeField] [Range(0, 5)] private float _maxRotation = 10f;
		private float _rotationPerFrame;
		private Quaternion _lastRotation = Quaternion.identity;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{

		}

		void Update()
		{
			if (_isRotating)
				Rotate();
		}

		void OnEnable()
		{
			_rotationPerFrame = Mathf.Lerp(_minRotation, _maxRotation, Random.value) * (Random.value < 0.5f ? -1f : 1f);
			_isRotating = true;
		}

		void OnDisable()
		{

		}

		void OnDestroy()
		{

		}
		#endregion

		private void Rotate() => transform.Rotate(new Vector3(0f, 0f, _rotationPerFrame));

		public void StartPullForward()
		{
			_isRotating = false;
			_lastRotation = transform.rotation;
		}

		public void PullForward(float chargePercent) => transform.rotation = Quaternion.Lerp(_lastRotation, Quaternion.identity, Mathf.SmoothStep(0f, 1f, chargePercent));
	}
}
