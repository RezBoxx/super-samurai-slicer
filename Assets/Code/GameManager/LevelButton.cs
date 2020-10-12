using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SSS { 
	public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
#pragma warning disable 0649
#pragma warning disable 0414

		public Image _imageComponent;
		private TrailRenderer _sliceLineTrail;
		
		private InputManager _inputManager;
				
		#region MonoBehavior
		void Start()
		{
			_imageComponent = GetComponent<Image>();
			_inputManager = FindObjectOfType<InputManager>();
			_sliceLineTrail = _inputManager._sliceLine.GetComponent<TrailRenderer>();
		}
		#endregion

		#region IPointerClickHandler
		public void OnPointerEnter(PointerEventData pointerEventData)
		{
			_sliceLineTrail.Clear();
			_inputManager._sliceLine.SetActive(true);
		}
		#endregion

		#region IPointerExitHandler
		public void OnPointerExit(PointerEventData pointerEventData)
		{			
			if (_inputManager._startLevel.gameObject.activeSelf == false && _inputManager._buttonCoolDown == _inputManager._coolDownTime && _inputManager != null)
			{
				_inputManager._level = int.Parse(gameObject.tag);
				_inputManager._startLevel.gameObject.SetActive(true);
				_inputManager._startLevelText.text = _inputManager._startLevelTextString + _inputManager._level + " ?";
				//Debug.Log(_inputManager._level);
				_imageComponent.sprite = _inputManager._cutSprite;

				GameController.s_instance.LevelStarted(_inputManager._level);
			}

			_inputManager._sliceLine.SetActive(false);
		}
		#endregion

		#region Functions
		#endregion
	}
}
