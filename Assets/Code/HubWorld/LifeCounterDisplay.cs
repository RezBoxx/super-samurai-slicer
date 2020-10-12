using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSS
{
	public class LifeCounterDisplay : MonoBehaviour
	{
		#region References
		#region public
		#endregion

		#region private
		private Text _text;
		#endregion
		#endregion

		#region Variables
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region MonoBehaviour
		void Awake()
		{
			//_text = GetComponent<Text>();
		}

		void Start()
		{
			//UpdateLifeDisplay(GameController.s_instance._howManyLives);
		}

		void Update()
		{
			
		}

		void OnEnable()
		{
			//GameController.s_onLivesChange += UpdateLifeDisplay;
		}

		void OnDisable()
		{
			//GameController.s_onLivesChange -= UpdateLifeDisplay;
		}

		void OnDestroy()
		{
			
		}
		#endregion

		private void UpdateLifeDisplay(int currentLives)
		{
			if (null != _text)
				_text.text = currentLives.ToString();
		}
	}
}
