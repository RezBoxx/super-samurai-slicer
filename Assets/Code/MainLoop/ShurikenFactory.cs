using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class ShurikenFactory : Singleton<ShurikenFactory>
	{
		#region References
		#region public
		#endregion

		#region private
		#endregion
		#endregion

		#region Variables
		#region public
		[SerializeField] public GameObject _prefab;
		#endregion

		#region private
		private ObjectPool _objectPoolBackingField;
		private ObjectPool _objectPool
		{
			get
			{
				if (null == _objectPoolBackingField)
				{
					if (ObjectPool.s_objectPools.ContainsKey(_prefab))
						_objectPoolBackingField = ObjectPool.s_objectPools[_prefab];
					else
						_objectPoolBackingField = ObjectPool.CreatePool<Shuriken>(_prefab, 10, transform);
				}

				return _objectPoolBackingField;
			}
			set => _objectPoolBackingField = value;
		}
		#endregion
		#endregion

		#region MonoBehaviour
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
			_objectPool = null;
			ObjectPool.ResetObjectPoolDictionary();
		}
		#endregion
		public void SpawnShuriken(Vector3 position, ThrowSide entrySide = ThrowSide.TOP)
		{
			if (_prefab == null)
			{
				Debug.Log("You are missing the prefab or a sprite in the ShurikenFactory.");
				return;
			}

			if (_objectPool == null)
			{
				Debug.Log("Pool is not there.");
			}

			Shuriken spawnedShuriken = (Shuriken)_objectPool.GetNextObject();

			if (null == spawnedShuriken)
			{
				Debug.Log("Couldn't give you a shuriken from pool " + ObjectPool.s_objectPools[_prefab] + " instead you got " + spawnedShuriken?.ToString());
				return;
			}

			spawnedShuriken._entrySide = entrySide;
			spawnedShuriken.ResetValues();
		}

		public void SpawnShuriken(Vector3[] path, float speed)
		{
			if (_prefab == null)
			{
				Debug.Log("You are missing the prefab or a sprite in the ShurikenFactory.");
				return;
			}

			if (_objectPool == null)
			{
				Debug.Log("Pool is not there.");
			}

			Shuriken spawnedShuriken = (Shuriken)_objectPool.GetNextObject();

			if (null == spawnedShuriken)
			{
				Debug.Log("Couldn't give you a shuriken from pool " + ObjectPool.s_objectPools[_prefab] + " instead you got " + spawnedShuriken?.ToString());
				return;
			}

			spawnedShuriken._entrySide = ThrowSide.SETPATH;
			spawnedShuriken.ResetValues();
			spawnedShuriken.SetPath(path);
			spawnedShuriken._flySpeed = speed;
		}
	}
}
