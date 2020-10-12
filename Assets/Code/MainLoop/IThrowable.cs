using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public interface IThrowable
	{
		#region Properties
		#endregion

		#region Methods
		void StartThrow();
		void ContinueFly();
		void CalculateNewPath();
		#endregion
	}
}
