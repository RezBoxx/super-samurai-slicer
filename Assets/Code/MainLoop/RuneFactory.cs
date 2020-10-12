using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSS
{
	#region Enums
	#endregion

	public class RuneFactory : Singleton<RuneFactory>
	{
#pragma warning disable 0649

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
		[SerializeField] private Sprite _spriteGoodRune;
		[SerializeField] private Sprite _spriteGoodRuneCharged;
		[SerializeField] private Sprite _spriteBadRune;
		[SerializeField] private Sprite _spriteBadRuneCharged;

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
						_objectPoolBackingField = ObjectPool.CreatePool<Rune>(_prefab, 10, transform);
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

		public void SpawnRune(Vector3 position, ThrowSide entrySide, bool isPlayerOwned = true)
		{
			if (_prefab == null && _spriteGoodRune != null && _spriteGoodRuneCharged != null && _spriteBadRune != null && _spriteBadRuneCharged != null)
			{
				Debug.Log("You are missing the prefab or a sprite in the RuneFactory.");
				return;
			}

			if (_objectPool == null)
			{
				Debug.Log("Pool is not there.");
			}

			Rune spawnedRune = (Rune)_objectPool.GetNextObject();

			if (null == spawnedRune)
			{
				Debug.Log("couldn't give you a rune from pool " + ObjectPool.s_objectPools[_prefab] + " instead you got " + spawnedRune.ToString());
				return;
			}

			spawnedRune._entrySide = entrySide;
			spawnedRune._isPlayerOwned = isPlayerOwned;
			spawnedRune.SetParticle(isPlayerOwned ? ParticleType.PLAYER : ParticleType.ENEMY);

			Button spawnedButton = spawnedRune.GetComponent<Button>();
			Image spawnedImage = spawnedRune._image;
			Image chargedImage = spawnedRune._chargedImage;

			if (null != spawnedButton && null != spawnedImage && null != chargedImage)
			{
				spawnedImage.sprite = isPlayerOwned ? _spriteGoodRune : _spriteBadRune;
				chargedImage.sprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;

				SpriteState ss = spawnedButton.spriteState;
				ss.highlightedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.pressedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.selectedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.disabledSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				spawnedButton.spriteState = ss;
			}

			spawnedRune.ResetValues();
		}

		public void SpawnRune(Vector3[] path, float speed)
		{
			bool isPlayerOwned = true;

			if (_prefab == null && _spriteGoodRune != null && _spriteGoodRuneCharged != null && _spriteBadRune != null && _spriteBadRuneCharged != null)
			{
				Debug.Log("You are missing the prefab or a sprite in the RuneFactory.");
				return;
			}

			if (_objectPool == null)
			{
				Debug.Log("Pool is not there.");
			}

			Rune spawnedRune = (Rune)_objectPool.GetNextObject();

			if (null == spawnedRune)
			{
				Debug.Log("couldn't give you a rune from pool " + ObjectPool.s_objectPools[_prefab] + " instead you got " + spawnedRune.ToString());
				return;
			}

			spawnedRune._entrySide = ThrowSide.SETPATH;
			spawnedRune._isPlayerOwned = isPlayerOwned;
			spawnedRune.SetParticle(isPlayerOwned ? ParticleType.PLAYER : ParticleType.ENEMY);

			Button spawnedButton = spawnedRune.GetComponent<Button>();
			Image spawnedImage = spawnedRune._image;
			Image chargedImage = spawnedRune._chargedImage;

			if (null != spawnedButton && null != spawnedImage && null != chargedImage)
			{
				spawnedImage.sprite = isPlayerOwned ? _spriteGoodRune : _spriteBadRune;
				chargedImage.sprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;

				SpriteState ss = spawnedButton.spriteState;
				ss.highlightedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.pressedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.selectedSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				ss.disabledSprite = isPlayerOwned ? _spriteGoodRuneCharged : _spriteBadRuneCharged;
				spawnedButton.spriteState = ss;
			}

			spawnedRune.ResetValues();
			spawnedRune.SetPath(path);
			spawnedRune._flySpeed = speed;
		}
	}
}
