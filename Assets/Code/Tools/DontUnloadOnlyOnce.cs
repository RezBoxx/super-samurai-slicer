using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSS
{
	public class DontUnloadOnlyOnce : MonoBehaviour
	{
		static bool _toggle = false;

		#region MonoBehaviour
		private void Awake()
		{
			if (!_toggle)
			{
				DontDestroyOnLoad(this.gameObject);
				_toggle = true;
			}
		}
		#endregion
	}
}
