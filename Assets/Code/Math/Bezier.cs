//using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

namespace SSS
{
	#region Enums
	#endregion

	/// <summary>
	/// Implementation of cubic Bézier splines.
	/// Unite talk: https://www.youtube.com/watch?v=o9RK6O2kOKo&start=32m41s
	/// </summary>
	public class Bezier : MonoBehaviour
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

		}
		#endregion

		public static Vector3 GetPoint(Vector3[] pts, float t)
		{
			if (pts.Length < 4)
			{
				Debug.Log("Can't construct curve with less than 4 control points.");
				return Vector3.forward;
			}

			t = Mathf.Clamp(t, 0f, 1f);

			float omt = 1f - t;
			float omt2 = omt * omt;
			float t2 = t * t;

			return pts[0] * (omt2 * omt) +
					pts[1] * (3f * omt2 * t) +
					pts[2] * (3f * omt * t2) +
					pts[3] * (t2 * t);
		}

		public static Vector3 GetTangent(Vector3[] pts, float t)
		{
			if (pts.Length < 4)
			{
				Debug.LogError("Can't construct curve with less than 4 control points.");
				return Vector3.forward;
			}

			t = Mathf.Clamp(t, 0f, 1f);

			float omt = 1f - t;
			float omt2 = omt * omt;
			float t2 = t * t;

			return (pts[0] * (-omt2) +
					pts[1] * (3f * omt2 - 2 * omt) +
					pts[2] * (-3f * t2 + 2 * t) +
					pts[3] * (t2)).normalized;
		}

		public static Vector3 GetNormal2D(Vector3[] pts, float t)
		{
			t = Mathf.Clamp(t, 0f, 1f);

			Vector3 tng = GetTangent(pts, t);
			return new Vector3(-tng.y, tng.x, 0f);
		}

		public static Vector3 GetNormal3D(Vector3[] pts, float t, Vector3 up)
		{
			t = Mathf.Clamp(t, 0f, 1f);

			Vector3 tng = GetTangent(pts, t);
			Vector3 binormal = Vector3.Cross(up, tng).normalized;
			return Vector3.Cross(tng, binormal);
		}

		public static Quaternion GetOrientation2D(Vector3[] pts, float t)
		{
			t = Mathf.Clamp(t, 0f, 1f);

			Vector3 tng = GetTangent(pts, t);
			Vector3 nrm = GetNormal2D(pts, t);
			return Quaternion.LookRotation(tng, nrm);
		}

		public static Quaternion GetOrientation3D(Vector3[] pts, float t, Vector3 up)
		{
			t = Mathf.Clamp(t, 0f, 1f);

			Vector3 tng = GetTangent(pts, t);
			Vector3 nrm = GetNormal3D(pts, t, up);
			return Quaternion.LookRotation(tng, nrm);
		}
	}
}
