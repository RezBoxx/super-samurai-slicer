using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class CameraShaker : MonoBehaviour
	{
		public static Action<float> s_cameraShake;

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
		private Vector3 originalPos;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{
			originalPos = transform.position;
			//Shake(20f);
		}

		void Update()
		{

		}

		void OnEnable()
		{
			s_cameraShake += Shake;
		}

		void OnDisable()
		{
			s_cameraShake -= Shake;
		}

		void OnDestroy()
		{

		}
		#endregion

		private void Shake(float duration) => StartCoroutine("RoutineShake", duration);

		public static void ShakeCamera(float duration) => CameraShaker.s_cameraShake?.Invoke(duration);

		private IEnumerable RoutineShake(float duration)
		{
			float elapsed = 0f;
			float magnitude = 1000f;

			while (elapsed < duration)
			{
				float x = UnityEngine.Random.Range(-1, 1f) * magnitude;
				float y = UnityEngine.Random.Range(-1, 1f) * magnitude;

				transform.localPosition = new Vector3(x, y, originalPos.z);

				elapsed += Time.deltaTime;

				yield return null;
			}

			transform.localPosition = originalPos;
		}
	}
}
