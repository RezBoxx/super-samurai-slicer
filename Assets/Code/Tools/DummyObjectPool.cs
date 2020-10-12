using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class DummyObjectPool : IObjectPoolant
	{
		#region References
		#region public
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

		#region IObjectPoolant
		public void FreeObject(GameObject element) => element.SetActive(false);

		public MonoBehaviour GetNextObject()
		{
			throw new NotImplementedException();
			//just instance it
		}

		public Type GetReferenceType()
		{
			throw new NotImplementedException();
		}

		public MonoBehaviour Resize()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
