using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class PopUpRefHolder : MonoBehaviour
	{
		#region References
		#region public
		[HideInInspector] public GameObject _vignette;
		[HideInInspector] public GameObject _winScreen;
		[HideInInspector] public GameObject _loseScreen;
		//public GameObject _settingsScreen;
		#endregion

		#region private
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
			_vignette = transform.GetChild(0)?.gameObject;
			_loseScreen = transform.GetChild(1)?.gameObject;
			_winScreen = transform.GetChild(2)?.gameObject;
			//_settingsScreen = transform.GetChild(2)?.gameObject;
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
	}
}
