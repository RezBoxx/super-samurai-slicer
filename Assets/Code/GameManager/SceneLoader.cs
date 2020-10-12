using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

namespace SSS
{
	/// <summary>
	/// Provides functions to load scenes.
	/// In the future shall also handle asynchronous loading.
	/// </summary>
	public class SceneLoader : Singleton<SceneLoader>
	{
		[System.Serializable]
		public class Level
		{
			#region Components
			[Header("Components")]
			public Sprite _background;
			public GameObject _enemy;
			#endregion

			#region Balancing
			[Header("Balancing")]
			public int _maxHealth = 100;
			public int _maxEnemyHealth = 100;
			public int _runeEnemyDmg = 10;
			public int _runePlayerDmg = 10;
			public int _shurikenDmg = 10;

			[Header("Spawning")]
			public int _minRunes = 1;
			public int _maxRunes = 6;
			public int _minShuriken = 0;
			public int _maxShuriken = 4;
			public float _minTime = 2f;
			public float _maxTime = 3f;
			#endregion
		}

		public void LoadLevel(int number)//input level number, not array number
		{
			if (null == _levels || _levels.Length == 0)
			{
				Debug.Log("You don't have levels.");
				return;
			}

			Level level = _levels[Mathf.Clamp(number - 1, 0, _levels.Length - 1)];

			if (null == level)
			{
				Debug.Log("Couldn't find your level.");
				return;
			}

			#region Components
			{
				Image dest = GameObject.Find("Background")?.GetComponent<Image>();
				if (null != dest)
					dest.sprite = level._background;
			}
			{
				if (null != level._enemy)
					GameObject.Instantiate(level._enemy);//, MatchController.s_instance._enemySpot);
			}
			#endregion

			#region Balancing & Spawning
			{
				MatchController match = MatchController.s_instance;

				match._playerMaxHealth = level._maxHealth;
				match._enemyMaxHealth = level._maxEnemyHealth;
				match.ResetHealth();

				match._minRunes = level._minRunes;
				match._maxRunes = level._maxRunes;
				match._minShuriken = level._minShuriken;
				match._maxShuriken = level._maxShuriken;
				match._minTime = level._minTime;
				match._maxTime = level._maxTime;

				Rune._thisLevelsRuneDmg = level._runeEnemyDmg;
				Rune._thisLevelsPlayerDmg = level._runePlayerDmg;
				Shuriken._thisLevelsProvDamage = level._shurikenDmg;
			}
			#endregion
		}

		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		[SerializeField] public Level[] _levels;
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

		#region Levels
		public void StartLevel(int number = 66)
		{
			if (number == 66)
				number = GameController.s_instance._whichLevel;


			GameController.s_instance.LevelStarted(number);
			SceneManager.LoadScene(1);

		}

		public static void RestartCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		public static void LoadHubWorld()
		{
			SceneManager.LoadScene("HubWorldTest");
		}
		#endregion
	}
}
