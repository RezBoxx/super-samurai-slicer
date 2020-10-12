using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class BlockFactory : Singleton<BlockFactory>
	{
#pragma warning disable CS0649
#pragma warning disable CS0414

		#region References
		#region public
		#endregion

		#region private
		private GameObject _leftBlock;
		private GameObject _rightBlock;
		#endregion
		#endregion

		#region Variables
		#region public
		#endregion

		#region private
		[SerializeField] private GameObject _blockPrefab;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{
			_leftBlock = transform.GetChild(0).gameObject;
			_rightBlock = transform.GetChild(1).gameObject;
			_leftBlock.SetActive(false);
			_rightBlock.SetActive(false);
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

		}
		#endregion

		public void SpawnBlock(Vector2 dim)
		{
			GameObject myBlock = GameObject.Instantiate(_blockPrefab, transform);
			//RectTransform myRect = myBlock.GetComponent<RectTransform>();
		}

		public void LeftBlock(bool onOrOff) => _leftBlock.SetActive(onOrOff);
		public void RightBlock(bool onOrOff) => _rightBlock.SetActive(onOrOff);

		public void DisableAllBlocks()
		{
			_leftBlock.SetActive(false);
			_rightBlock.SetActive(false);
		}
	}
}
