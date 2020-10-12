using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	public enum ParticleType { PLAYER, ENEMY, SHURIKEN, PETAL, RUNE}
	#endregion

	public class ParticleFactory : Singleton<ParticleFactory>
	{
#pragma warning disable 0649

		public static Action<ParticleType, Vector3> s_spawnParticle;

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
		[SerializeField] private GameObject _playerParticle;
		[SerializeField] private GameObject _enemyParticle;
		[SerializeField] private GameObject _runeParticle;

		private ObjectPool _objectPoolBackingFieldPlayer;
		private ObjectPool _objectPoolPlayer
		{
			get
			{
				if (null == _objectPoolBackingFieldPlayer)
				{
					if (ObjectPool.s_objectPools.ContainsKey(_playerParticle))
						_objectPoolBackingFieldPlayer = ObjectPool.s_objectPools[_playerParticle];
					else
						_objectPoolBackingFieldPlayer = ObjectPool.CreatePool<ParticleRefHolder>(_playerParticle, 10, transform);
				}

				return _objectPoolBackingFieldPlayer;
			}
			set => _objectPoolBackingFieldPlayer = value;
		}

		private ObjectPool _objectPoolBackingFieldEnemy;
		private ObjectPool _objectPoolEnemy
		{
			get
			{
				if (null == _objectPoolBackingFieldEnemy)
				{
					if (ObjectPool.s_objectPools.ContainsKey(_enemyParticle))
						_objectPoolBackingFieldEnemy = ObjectPool.s_objectPools[_enemyParticle];
					else
						_objectPoolBackingFieldEnemy = ObjectPool.CreatePool<ParticleRefHolder>(_enemyParticle, 10, transform);
				}

				return _objectPoolBackingFieldEnemy;
			}
			set => _objectPoolBackingFieldEnemy = value;
		}

		private ObjectPool _objectPoolBackingFieldRune;
		private ObjectPool _objectPoolRune
		{
			get
			{
				if(null == _objectPoolBackingFieldRune)
				{
					if (ObjectPool.s_objectPools.ContainsKey(_runeParticle))
						_objectPoolBackingFieldRune = ObjectPool.s_objectPools[_runeParticle];
					else
						_objectPoolBackingFieldRune = ObjectPool.CreatePool<ParticleRefHolder>(_runeParticle, 10,transform);
				}
				return _objectPoolBackingFieldRune;
			}
			set => _objectPoolBackingFieldRune = value;
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
			s_spawnParticle += SpawnParticle;
		}

		void OnDisable()
		{
			s_spawnParticle -= SpawnParticle;
		}

		void OnDestroy()
		{
			_objectPoolPlayer = null;
			_objectPoolEnemy = null;
			ObjectPool.ResetObjectPoolDictionary();
		}
		#endregion

		public void SpawnParticle(ParticleType particleType, Vector3 pos)
		{
			ParticleRefHolder part;

			if (particleType == ParticleType.PLAYER)
			{
				part = (ParticleRefHolder)_objectPoolPlayer.GetNextObject();
				part.transform.position = pos;
				part._particleSystem.Play();
			}
			else if (particleType == ParticleType.ENEMY)
			{
				part = (ParticleRefHolder)_objectPoolEnemy.GetNextObject();
				part.transform.position = pos;
				part._particleSystem.Play();
			}
			else if(particleType == ParticleType.RUNE)
			{
				part = (ParticleRefHolder)_objectPoolRune.GetNextObject();
				part.transform.position = pos;
				part._particleSystem.Play();
			}
		}
	}
}
