using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class ParticleRefHolder : MonoBehaviour
	{
		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		public ParticleSystem _particleSystem;
		#endregion

		#region private
		#endregion
		#endregion

		#region MonoBehaviour
		void Awake()
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}

		void Start()
		{
			
		}

		void Update()
		{
			
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
	}
}
