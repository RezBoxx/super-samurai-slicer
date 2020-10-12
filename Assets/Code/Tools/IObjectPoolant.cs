using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public interface IObjectPoolant
	{
		#region Properties
		#endregion

		#region Methods
		//public  static ObjectPool CreatePool<T>(ObjectType type, int initialSize, Transform grandParent) where T : MonoBehaviour;
		MonoBehaviour GetNextObject();
		void FreeObject(GameObject element);
		MonoBehaviour Resize();
		System.Type GetReferenceType();
		#endregion
	}
}
