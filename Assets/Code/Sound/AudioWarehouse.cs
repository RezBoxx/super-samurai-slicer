using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	public enum UiSounds { DEFAULT, BUTTON, BACK, };
	public enum EnemySounds { DEFAULT, ATTACK, DEFEND, GETHIT, DIE, };
	public enum PlayerSounds { DEFAULT, ATTACK, DEFEND, GETHIT, WIN, LOSE, };
	#endregion

	/// <summary>
	/// Holds all audioclips and provides accessors.
	/// </summary>
	public class AudioWarehouse : Singleton<AudioWarehouse>
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
		#endregion
		private AudioClip _defaultClipBackingField;
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
