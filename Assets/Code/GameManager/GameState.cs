using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	/// <summary>
	/// Everything that needs to be saved in order to recreate a GameState.
	/// </summary>
	public class GameState
	{
		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		public int _lives = 9;
		public int _finishedLevels = 0;
		public int _currentLevel = 1;
		#endregion

		#region private
		#endregion
		#endregion

		public override string ToString()
		{
			return $"Current GameState: LIVES: {_lives} | LEVEL BEATEN: {_finishedLevels} | CURRENT LEVEL: {_currentLevel}.";
		}
	}
}
