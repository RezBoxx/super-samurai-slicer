using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public class DontUnload : MonoBehaviour
	{
		#region MonoBehaviour
		private void Awake() => DontDestroyOnLoad(this.gameObject);
		#endregion
	}
}
