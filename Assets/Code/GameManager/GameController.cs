using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class GameController : Singleton<GameController>
	{
		public static Action<int> s_onLivesChange;
		//public static int s_currentLevel = 99;

		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		public bool _playerHasLives { get => 0 < _currentGamestate._lives; }
		public int _howManyLives { get => _currentGamestate._lives; }
		public int _whichLevel { get => _currentGamestate._currentLevel; }
		#endregion

		#region private
		[SerializeField] private GameState _currentGamestate = new GameState();
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

		#region Lifes
		public void GainLife()
		{
			//	_currentGamestate._lives++;
			//	//Debug.Log("You gained a life!");
			//	OnLifeChangedAction();
		}

		public void LoseLife()
		{
			//_currentGamestate._lives--;
			//OnLifeChangedAction();
		}

		private void OnLifeChangedAction() => s_onLivesChange?.Invoke(_currentGamestate._lives);
		#endregion

		#region Progression
		public void LevelStarted(int level)
		{
				_currentGamestate._currentLevel = level;
		}

		public void LevelBeaten()
		{
			if (_currentGamestate._finishedLevels < _currentGamestate._currentLevel)
				_currentGamestate._finishedLevels = _currentGamestate._currentLevel;
		}
		#endregion

		#region Save & Load
		public void LoadGameState(GameState newGameState) => _currentGamestate = newGameState;
		#endregion
	}
}
