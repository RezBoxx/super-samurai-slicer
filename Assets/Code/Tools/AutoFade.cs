using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSS
{
	public class AutoFade : MonoBehaviour
	{
		public static Action<int> s_resetFade;

		#region References
		#region public
		#endregion

		#region private
		private Image _image;
		#endregion
		#endregion

		#region Variables
		#region public
		#endregion

		#region private
		private float _maxAlpha;
		#endregion
		#endregion

		#region MonoBehaviour
		void Awake()
		{
			_image = GetComponent<Image>();

			if (null != _image)
				_maxAlpha = _image.color.a;
		}

		void Start()
		{

		}

		void Update()
		{
			Color col = _image.color;
			col.a = Mathf.Max(0, col.a - Time.deltaTime);
			_image.color = col;
		}

		void OnEnable()
		{
			s_resetFade += ResetFade;
			ResetFade();
		}

		void OnDisable()
		{
			s_resetFade -= ResetFade;
		}

		void OnDestroy()
		{

		}
		#endregion

		public void ResetFade(int noUse = 0)
		{
			if (null != _image)
			{
				Color col = _image.color;
				col.a = _maxAlpha;
				_image.color = col;
			}
		}
	}
}
