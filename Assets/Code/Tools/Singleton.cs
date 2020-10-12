﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T s_instanceBackingField = default;
		public static T s_instance
		{
			get
			{
				if (s_instanceBackingField == null)
				{
#if DB_ST
					Debug.Log("no object of type " + typeof(T) + " was found.");
#endif
					GameObject tmp = new GameObject("SINGLETON_" + typeof(T));
					s_instanceBackingField = tmp.AddComponent<T>();
				}
				return s_instanceBackingField;
			}
		}

		protected void Awake()
		{
			if (s_instanceBackingField != null)
			{
#if DB_ST
				Debug.Log("Multiple instances of type " + typeof(T));
#endif
				Destroy(this);
				return;
			}
			s_instanceBackingField = this as T;
		}

		public static bool Exists() => s_instanceBackingField != default;
	}
}