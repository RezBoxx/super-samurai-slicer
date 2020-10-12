using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	/// <summary>
	/// Handles audiosources. Provides functions to call to play audioclips.
	/// </summary>
	public class AudioController : MonoBehaviour
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
		private AudioSource _musicSource = new AudioSource();
		private AudioSource _sfxSource = new AudioSource();
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

		//private void PlaySFX() => _sfxSource.PlayOneShot(AudioWarehouse.s_instance._defaultClip);
	}
}
