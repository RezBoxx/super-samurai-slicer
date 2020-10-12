using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	public enum RuneOrShuriken { RUNE, SHURIKEN }
	public enum PatternName { PATTERN_0, PATTERN_1, PATTERN_2, PATTERN_3, PATTERN_4, PATTERN_5, PATTERN_6, PATTERN_7, PATTERN_8, PATTERN_9, }
	#endregion

	public class PatternBase : Singleton<PatternBase>
	{
#pragma warning disable CS0649
#pragma warning disable CS0414

		[Serializable]
		public class Yeetable
		{
			[SerializeField] public RuneOrShuriken _type = RuneOrShuriken.RUNE;
			[SerializeField] public ThrowSide _entrySide = ThrowSide.BOTTOM;
			[SerializeField] [Tooltip("0: bottom/left\n 1: top/right")] [Range(0f, 1f)] public float _entryPoint = 0.5f;
			[SerializeField] public ThrowSide _exitSide = ThrowSide.BOTTOM;
			[SerializeField] [Tooltip("0: bottom/left\n 1: top/right")] [Range(0f, 1f)] public float _exitPoint = 0.5f;
			[SerializeField] [Range(0.1f, 10f)] public float _speed = 1f;
			[SerializeField] [Tooltip("0: start of pattern\n 1: end of pattern")] [Range(0f, 1f)] public float _spawnTime = 0.5f;
		}

		[Serializable]
		public class Pattern
		{
			public PatternName _name = PatternName.PATTERN_0;
			[Range(0.1f, 10f)] public float _totalTime = 3f;
			[SerializeField] public Yeetable[] _yeetables;
		}

		[Serializable]
		public class Level
		{
			[SerializeField] public PatternName[] _patterns;
		}

		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		[SerializeField] public Pattern[] _patternDefinitions;
		[SerializeField] public Level[] _levels;
		#endregion

		#region private
		#endregion
		private Dictionary<PatternName, Pattern> _dictionary = new Dictionary<PatternName, Pattern>();
		#endregion

		#region MonoBehaviour
		void Start()
		{
			for (int i = 0; i < _patternDefinitions?.Length; i++)
				_dictionary[_patternDefinitions[i]._name] = _patternDefinitions[i];
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

		public Pattern GetPattern(PatternName name) => _dictionary.ContainsKey(name) ? _dictionary[name] : new Pattern();
	}
}
