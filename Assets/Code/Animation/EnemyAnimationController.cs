using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace SSS
{
	#region Enums
	public enum EnemyAnimations { IDLE, ATTACK, DEFEND, TAKEDAMAGE, DIE };
	#endregion

	/// <summary>
	/// Provides functions to put enemy in a visual state.
	/// </summary>
	public class EnemyAnimationController : MonoBehaviour
	{
		#region References
		#region public
		public SkeletonAnimation _skeletonAnimation;
		public AnimationReferenceAsset _idleAnim;
		public AnimationReferenceAsset _gettingHitAnim;
		public AnimationReferenceAsset _blockAnim;
		public AnimationReferenceAsset _attackAnim;
		public AnimationReferenceAsset _deathAnim;

		public int testint;

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

		#region MonoBehaviour
		void Start()
		{
			Initialise();
		}

		void Update()
		{
			if (_skeletonAnimation == null)
				Initialise();
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

		public void Initialise()
		{
			_skeletonAnimation = FindObjectOfType<SkeletonAnimation>();
			MatchController.s_instance._enemyAnimationController = this;

			if (null == _skeletonAnimation)
				return;

			_skeletonAnimation.state.AddAnimation(0, _idleAnim, true, 0f);
		}

		public void PlayAnimation(AnimationReferenceAsset animation, bool loop, float timeScale = 1f)
		{
			if (null == _skeletonAnimation || null == animation)
				return;

			_skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
			_skeletonAnimation.state.AddAnimation(0, _idleAnim, true, 0f).TimeScale = timeScale;
		}

		public void Yeet() => _skeletonAnimation.transform.position += Vector3.right * 100f;
	}
}
