using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	/// <summary>
	/// Holds and invokes the coreloop actions.
	/// Needs to know when a part of the coreloop is finished, so it can start the next.
	/// </summary>
	public class LoopRunner : MonoBehaviour
	{
		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		public static Action<int> s_Setup;
		public static Action<int> s_PlayerAttacks;
		public static Action<int> s_PlayerDefends;
		#endregion

		#region private
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
