using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class ButtonController : Singleton<ButtonController>
	{
		#region References
		#region public
		#endregion

		#region private
		private PopUpRefHolder _popUps;
		#endregion
		#endregion

		#region Variables
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region MonoBehaviour
		new void Awake()
		{
			base.Awake();
			_popUps = FindObjectOfType<PopUpRefHolder>();

			if (null == _popUps)
				Debug.LogError("I couldn't find a PopUpRefHolder. Sorries.");
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

		#region PopUps
		public void TurnOffAllPopUps()
		{
			_popUps._loseScreen.SetActive(false);
			_popUps._winScreen.SetActive(false);
			//_popUps._settingsScreen.SetActive(false);
		}

		public void Vignette() => _popUps._vignette.SetActive(true);
		public void LoseScreen() => _popUps._loseScreen.SetActive(true);
		public void WinScreen() => _popUps._winScreen.SetActive(true);
		//public void SettingsScreen() => _popUps._settingsScreen.SetActive(true);
		#endregion

		#region ButtonPresses
		public void SettingsButton()
		{
			Debug.Log("pressed settings");
		}

		public void HomeButton()
		{
			//Debug.Log("pressed home");
			SceneLoader.LoadHubWorld();
		}

		public void RetryButton()
		{
			//Debug.Log("pressed retry");
			SceneLoader.RestartCurrentLevel();
		}
		
		public void NextLevelButton()
		{
			Debug.Log("next level is still restart");
			GameController.s_instance.LevelStarted(Mathf.Min(20, GameController.s_instance._whichLevel + 1));
			RetryButton();
		}
		#endregion
	}
}
