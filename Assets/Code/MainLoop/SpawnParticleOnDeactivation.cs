using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public class SpawnParticleOnDeactivation : MonoBehaviour
	{
		#region Variables
		#region public
		public ParticleType _particleType;
		#endregion
		#endregion

		#region MonoBehaviour
		void OnDisable()
		{
			ParticleFactory.s_spawnParticle?.Invoke(_particleType, transform.position);
		}
		#endregion
	}
}
