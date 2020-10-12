using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	public class Block
	{
#pragma warning disable 0649

		public Block(float xLeft, float xRight, float maxDuration)
		{
			_xLeft = xLeft;
			_xRight = xRight;
			_ticker = 0f;
			_maxDuration = maxDuration;
		}

		public float _xLeft = 0f;
		public float _xRight = 1f;
		public float _ticker = 0f;
		public float _maxDuration = 2f;
	}

	public class BlockController : Singleton<BlockController>
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
		#endregion

		#region private
		private List<Block> _blocks = new List<Block>();
		[SerializeField] [Tooltip("How long does the block last?")] private float _blockDuration = 2f;
		[SerializeField] [Tooltip("How much does slicing a rune charge the block bar? Every 1 charge a block appears.")] [Range(0.1f, 1f)] private float _chargePerRune;
		private float _charge = 0f;
		#endregion
		#endregion

		#region MonoBehaviour
		void Start()
		{
			_blocks.Clear();
			_charge = 0f;
		}

		void Update()
		{
			_charge = Mathf.Clamp(_charge - 0.01f * Time.deltaTime, 0f, 1f);
			Tick();
			UpdateChargeBarVisualisation();
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

		#region Blocking
		public void Tick()
		{
			if (_blocks.Count == 0)
				return;

			List<Block> killList = new List<Block>();

			foreach (Block block in _blocks)
			{
				block._ticker += Time.deltaTime;

				if (block._maxDuration < block._ticker)
					killList.Add(block);
			}

			if (0 == killList.Count)
				return;

			foreach (Block block in killList)
				_blocks.Remove(block);

			BlockFactory.s_instance.DisableAllBlocks();
			UpdateBlockVisualisation();
		}

		public bool WasIBlocked(Vector3 pos) => WasIBlocked(MatchController.s_instance.RelativeHorizontalPosition(pos.x));

		public bool WasIBlocked(float X)
		{
			if (_blocks.Count == 0)
				return false;

			foreach (Block block in _blocks)
			{
				if (block._xLeft <= X && X <= block._xRight)
					return true;
			}

			return false;
		}

		public void CreateBlock(float xLeft, float xRight) => _blocks.Add(new Block(xLeft, xRight, _blockDuration));
		public void CreateBlock(bool leftOrRight)
		{
			if (leftOrRight)
			{
				CreateBlock(0f, 0.5f);
				BlockFactory.s_instance.LeftBlock(true);
			}
			else
			{
				CreateBlock(0.5f, 1f);
				BlockFactory.s_instance.RightBlock(true);
			}
		}

		public void RemoveAllBlocks()
		{
			_blocks.Clear();
			BlockFactory.s_instance.DisableAllBlocks();
		}

		public void IncreaseCharge()
		{
			if (0 < _blocks.Count)//early out if there already is a block active
				return;

			//_charge += _chargePerRune * UnityEngine.Random.value * 2f;
			_charge += _chargePerRune * (0.5f + UnityEngine.Random.value);

			if (1f <= _charge)
			{
				_charge = 0f;

				//Vector2 blockPattern = GetBlockPattern();
				//CreateBlock(blockPattern.x, blockPattern.y);
				//UpdateBlockVisualisation();

				CreateBlock(0 < UnityEngine.Random.Range(0, 2));

				/*if (null != MatchController.s_instance._enemyAnimationController)
					MatchController.s_instance._enemyAnimationController.PlayAnimation(MatchController.s_instance._enemyAnimationController._blockAnim, false, 1f);*/
			}

			UpdateChargeBarVisualisation();
		}

		private Vector2 GetBlockPattern() => new Vector2(0.2f, 0.8f);
		#endregion

		#region Visualisation
		private void UpdateChargeBarVisualisation() => MatchController.s_instance.BlockSlider(_charge * 100f);

		private void UpdateBlockVisualisation()
		{

		}
		#endregion
	}
}
