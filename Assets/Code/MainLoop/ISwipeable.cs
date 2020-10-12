using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public interface ISwipeable
	{
		#region Properties
		bool _isSwipeable { get; set; }
		#endregion

		#region Methods
		void Activate();
		void Swipe();
		void Deactivate();
		#endregion
	}
}
