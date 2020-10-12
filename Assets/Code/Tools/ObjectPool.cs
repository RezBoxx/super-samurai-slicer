using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

namespace SSS
{
	/// <summary>
	/// Forked and butchered from PPBA/ObjectPool.cs on 18.05.2020
	/// https://github.com/Assertores/PewPew-BattlefieldArchitect-
	/// Props and love out to Assertores.
	/// </summary>
	public class ObjectPool : IObjectPoolant
	{
		/// <summary>
		/// CreatePool(PREFAB, SIZE, PARENT)
		/// s_objectPools[PREFAB].GetNextObject(); => Gives you an available object of the objectpool, and will turn it on. If none are available, the object pool will resize.
		/// FreeObject(ELEMENT) will return the element back to the pool, but the same is achieved by deactivating the gameobject
		/// </summary>
		public static Dictionary<GameObject, ObjectPool> s_objectPools { get; private set; } = new Dictionary<GameObject, ObjectPool>();
		public static Dictionary<ObjectPool, string> s_grandParentsName { get; private set; } = new Dictionary<ObjectPool, string>();

		private GameObject _prefab;
		private List<MonoBehaviour> _elements = new List<MonoBehaviour>();
		private System.Type _type;
		private Transform _parent;
		private int _stepSize;

		ObjectPool(GameObject prefab, int initialSize, Transform parent, System.Type type)
		{
			_prefab = prefab;
			_parent = parent;
			_stepSize = initialSize;
			_type = type;

			if (_stepSize <= 0)
				return;

			Resize().gameObject.SetActive(false);
		}

		/// <summary>
		/// creates an objectPool with a prefab as key
		/// </summary>
		/// <typeparam name="T">the type as MonoBehaviour, the prefab hast to have as component on the top level</typeparam>
		/// <param name="prefab">the prefab, that should be used for the object pool</param>
		/// <param name="initialSize">the size of the object pool</param>
		/// <param name="grandParent">the parent object in witch the objectpool will initialice in objectholder in witch the objects will be instanciated into</param>
		/// <returns>the objectpool of the prefab type. null if prefab is null, prefab has not T component at the top level, or the prefab has no INetElement on any level if it is flaged as doTrackInNetwork</returns>
		public static ObjectPool CreatePool<T>(GameObject prefab, int initialSize, Transform grandParent) where T : MonoBehaviour
		{
			if (prefab == null)
				return null;
			if (s_objectPools.ContainsKey(prefab))
				return s_objectPools[prefab];
			if (!prefab.GetComponent(typeof(T)))
				return null;

			Transform tmp = ConstructParent(prefab.name, grandParent);
			tmp.transform.localScale = new Vector3(1f, 1f, 1f);
			s_objectPools[prefab] = new ObjectPool(prefab, initialSize, tmp, typeof(T));
			s_grandParentsName[s_objectPools[prefab]] = grandParent.name;
			return s_objectPools[prefab];
		}

		private static Transform ConstructParent(string name, Transform grandParent)
		{
			GameObject tmp = new GameObject(name);
			tmp.transform.parent = grandParent;
			return tmp.transform;
		}

		public static void ResetAllObjectPools()
		{
			foreach (var it in s_objectPools)
			{
				it.Value.ResetObjectPool();
			}
		}

		public void ResetObjectPool()
		{
			foreach (Transform it in _parent)
			{
				it.gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// Use this to get a free element in the object pool which will be automatically be set to active.
		/// </summary>
		/// <param name="team">the team that should be applied to the IRefHolder if set</param>
		/// <returns>the reference to the type as MonoBehaviour</returns>
		public MonoBehaviour GetNextObject()
		{
			if (null == _parent)
			{
				GameObject grandParent = GameObject.Find(s_grandParentsName[this]);
				_parent = ConstructParent(_prefab.name, grandParent.transform);
			}

			for (int i = 0; i < _parent.childCount; i++)
			{
				if (!_parent.GetChild(i).gameObject.activeSelf)
				{
					_parent.GetChild(i).gameObject.SetActive(true);
					return _elements[i];
				}
			}

			return Resize();
		}

		public void FreeObject(GameObject element)//not really elegant
		{
			for (int i = 0; i < _parent.childCount; i++)
			{
				if (_parent.GetChild(i).gameObject == element)
				{
					element.SetActive(false);
					return;
				}
			}
		}

		public MonoBehaviour Resize()
		{
			GameObject firstElement = GameObject.Instantiate(_prefab, _parent);
			firstElement.name = _prefab.name + " (" + _parent.childCount + ")";

			MonoBehaviour value = firstElement.GetComponent(_type) as MonoBehaviour;
			_elements.Add(value);

			for (int i = 1; i < _stepSize; i++)
			{
				GameObject tmp = GameObject.Instantiate(_prefab, _parent);
				tmp.SetActive(false);
				tmp.name = _prefab.name + " (" + _parent.childCount + ")";

				MonoBehaviour script = tmp.GetComponent(_type) as MonoBehaviour;
				_elements.Add(script);
			}

			return value;
		}

		/// <summary>
		/// use this to safely cast the MonoBehaviour if you don't know the correct type
		/// </summary>
		/// <returns>the type to which the MonoBehaviour can be cast</returns>
		public System.Type GetReferenceType() => _type;

		public static void ResetObjectPoolDictionary() => s_objectPools = new Dictionary<GameObject, ObjectPool>();
	}
}
