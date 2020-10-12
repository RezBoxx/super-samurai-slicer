using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public class AutoOff : MonoBehaviour
	{
		#region Variables
		#region public
		#endregion

		#region private
		[SerializeField] private float _delay = 1f;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{
			
		}

		void Update()
		{
			
		}

		void OnEnable()
		{
			StartCoroutine("DelayedDeactivation", _delay);
		}

		void OnDisable()
		{
			
		}

		void OnDestroy()
		{
			
		}
		#endregion

		private IEnumerator DelayedDeactivation(float delay)
		{
			yield return new WaitForSeconds(delay);
			gameObject.SetActive(false);
		}
	}
}
