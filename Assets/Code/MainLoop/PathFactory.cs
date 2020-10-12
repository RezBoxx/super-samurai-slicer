using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSS
{
	#region Enums
	public enum ThrowSide { BOTTOM, LEFT, RIGHT, TOP, SETPATH }
	public enum ThrowTimings { UNIFORM, CLUSTERED_ONE, CLUSTERED_TWO, CLUSTERED_THREE, SPACED }
	#endregion

	public static class PathFactory
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

		public static ThrowSide RandomThrowSide()//doesn't return TOP
		{
			float r = Random.value;

			if (r <= 0.5f)
				return ThrowSide.BOTTOM;
			else if (r <= 0.75f)
				return ThrowSide.LEFT;
			else
				return ThrowSide.RIGHT;
		}

		public static ThrowTimings RandomThrowTimingType()
		{
			float r = Random.value;

			if (r <= 0.5f)
				return ThrowTimings.UNIFORM;
			else if (r <= 0.7f)
				return ThrowTimings.SPACED;
			else if (r <= 0.9f)
				return ThrowTimings.CLUSTERED_ONE;
			else if (r <= 0.95f)
				return ThrowTimings.CLUSTERED_TWO;
			else
				return ThrowTimings.CLUSTERED_THREE;
		}

		public static Vector3[] MakePath(ThrowSide side)
		{
			Vector3[] path = new Vector3[4];

			switch (side)
			{
				case ThrowSide.BOTTOM:
					path[0] = MatchController.s_instance.RandomBottomPoint();
					path[3] = MatchController.s_instance.RandomBottomPoint();
					path[1] = new Vector3(Mathf.Lerp(path[0].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					path[2] = new Vector3(Mathf.Lerp(path[1].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					break;
				case ThrowSide.LEFT:
					path[0] = MatchController.s_instance.RandomLeftPoint();
					path[3] = MatchController.s_instance.RandomBottomRightPoint();
					path[1] = new Vector3(Mathf.Lerp(path[0].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					path[2] = new Vector3(Mathf.Lerp(path[1].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					break;
				case ThrowSide.RIGHT:
					path[0] = MatchController.s_instance.RandomRightPoint();
					path[3] = MatchController.s_instance.RandomBottomLeftPoint();
					path[1] = new Vector3(Mathf.Lerp(path[0].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					path[2] = new Vector3(Mathf.Lerp(path[1].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[1].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					break;
				case ThrowSide.TOP:
					path[0] = MatchController.s_instance.RandomTopPoint();
					path[3] = MatchController.s_instance.RandomTopPoint();
					path[1] = new Vector3(Mathf.Lerp(path[0].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[0].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					path[2] = new Vector3(Mathf.Lerp(path[1].x, path[3].x, Random.value), MatchController.s_instance._panelCorners[0].y + 0.5f * (0.5f - Random.value) * (MatchController.s_instance._panelCorners[0].y - MatchController.s_instance._panelCorners[1].y), path[0].z);
					break;
				default:
					break;
			}

			return path;
		}

		public static float[] MakeTimings(ThrowTimings type, float totalTime, int count = 1)
		{
			count = Mathf.Max(1, count);

			float[] timings = new float[count];

			switch (type)
			{
				case ThrowTimings.UNIFORM:
					for (int i = 0; i < count; i++)
						timings[i] = totalTime * Random.value;
					break;
				case ThrowTimings.CLUSTERED_ONE:
					timings[0] = totalTime * Random.value;
					for (int i = 1; i < count; i++)
						timings[i] = timings[0] + (0.5f - Random.value) * (totalTime / 10f);
					break;
				case ThrowTimings.CLUSTERED_TWO:
					timings[0] = totalTime * Random.value;
					if (timings.Length < 2)
					{
						Debug.Log("Not enough timings requested to make two clusters. Defaulting to uniform distribution.");
						return MakeTimings(ThrowTimings.UNIFORM, totalTime, count);

					}
					timings[1] = totalTime * Random.value;
					for (int i = 2; i < count; i++)
					{
						timings[i] = timings[Random.Range(0, 2)] + (0.5f - Random.value) * (totalTime / 20f);
					}
					break;
				case ThrowTimings.CLUSTERED_THREE:
					timings[0] = totalTime * Random.value;
					if (timings.Length < 3)
					{
						Debug.Log("Not enough timings requested to make three clusters. Defaulting to uniform distribution.");
						return MakeTimings(ThrowTimings.UNIFORM, totalTime, count);
					}
					timings[1] = totalTime * Random.value;
					timings[2] = totalTime * Random.value;
					for (int i = 3; i < count; i++)
						timings[i] = timings[Random.Range(0, 3)] + (0.5f - Random.value) * (totalTime / 20f);
					break;
				case ThrowTimings.SPACED:
					for (int i = 0; i < count; i++)
						timings[i] = i / count;
					break;
				default:
					break;
			}

			return timings;
		}
	}
}
