using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace SSS
{
	#region Enums
	#endregion

	/// <summary>
	/// Handles swiping and tipping.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
#pragma warning disable 0649
#pragma warning disable 0414
		#region References
		#region public
		public GameObject _slice;
		public Sprite _cutSprite;
		public GameObject _startLevel;
		public Text _startLevelText;
		public Sprite _startSprite;
		public GameObject _sliceLine;
		public Touch _playerInput;
		#endregion
		#region private
		[SerializeField]
		private Sprite _tickImage;
		[SerializeField]
		private Sprite _soundButtonImage;
		[SerializeField]
		private Button _soundButton;
		[SerializeField]
		private GameObject[] _buttons;
		private TrailRenderer _sliceTrail;
		#endregion
		#endregion
		#region Variables
		#region public
		public string _startLevelTextString;
		public int _level;
		public bool _canSlice;
		public float _buttonCoolDown;
		public float _coolDownTime;
		#endregion
		#region private
		[SerializeField]private float _sliceDistance;
		private bool _soundTogle;
		private Queue<Vector3> _mousePosition = new Queue<Vector3>();
		private int _queueCount = 7;
		private Vector3 _playerTouchStart;



		#endregion
		#endregion
		#region MonoBehaviour
		void Start()
		{
			_sliceTrail = _slice.GetComponent<TrailRenderer>();
			//_slice.gameObject.SetActive(false);
		}

		void Update()
		{

			if (null == _slice)
			{
				Debug.Log("Slice is null. Why?");
				_slice = GameObject.Find("Slice");
				if (null != _slice)
					Debug.Log("I found a slice! It was expansive though.");
				else
					return;
			}
			/*if (Input.GetMouseButtonDown(0))
			{
				//Cursor.visible = true;
				_slice.gameObject.SetActive(true);
				_playerTouchStart = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(0)) 
			{
				_canSlice = false;
				
				_slice.gameObject.SetActive(false);
				_playerTouchStart = Vector3.zero;

			}*/
			Cursor.visible = false;
			float dist = Vector3.Distance(_playerTouchStart,Input.mousePosition);
			if (dist>= _sliceDistance)
				_canSlice = true;
			
			if (Input.touchCount > 0)
			{
				_playerInput = Input.GetTouch(0);
			}

			_buttonCoolDown++;
			_slice.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1));
			if (_buttonCoolDown >= _coolDownTime)
			{
				_buttonCoolDown = _coolDownTime;
			}
			//_slice.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 1);
			//Justin Prototype Code
			/* 
			
			_mousePosition.Enqueue(Input.mousePosition);
			if(_mousePosition.Count > _queueCount)
			{
				Vector3 lastmouseposition = _mousePosition.Dequeue();
				_slice.transform.position = lastmouseposition;
				Vector2 dir = (_slice.transform.position - Input.mousePosition).normalized;
				float angle = Mathf.Atan2(dir.y, dir.x);
				_slice.transform.localEulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg + _angleCorrecter);
			}

			/*if (Input.touchCount > 0)
			{
                Touch touch = Input.GetTouch(0);

				switch (touch.phase)
                {
					case TouchPhase.Began:
                        
						break;
                    case TouchPhase.Moved:                        
                            
                        break;
                    case TouchPhase.Ended :
                        
                        break;
                }
				
			}
			//Justin Prototype Code
			
			if (Input.GetMouseButtonDown(0)|| Input.touchCount > 0)
			{	
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit,100))
				{
					if (hit.collider != null)
					{
						_startLevelPanel.gameObject.SetActive(true);
						_startLevelText.text = _startLevelTextString + hit.collider.tag + " ?";
						_level = int.Parse(hit.collider.tag);
						Debug.Log(_level);
					}
				}
			}
			
			if (Input.GetMouseButtonDown(0))
			{
	
				_dragPosition = Input.mousePosition;
				
				return;
			}
			if (!Input.GetMouseButton(0)) return;
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragPosition);
			float clamp = Mathf.Clamp(pos.y,_minClamp,_maxClamp);
			Vector3 move = new Vector3(0,pos.y * _dragSpeed,0);
			Vector3 cameraClamp = Camera.main.transform.position;
			cameraClamp.y = Mathf.Clamp(cameraClamp.y, _minClamp, _maxClamp);
			Camera.main.transform.position = cameraClamp;


			Camera.main.transform.Translate(move, Space.World);*/
			//Justin Prototype Code
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
		#region Functions
		public void ResetImage()
		{
			foreach (GameObject button in _buttons)
			{
				button.GetComponent<LevelButton>()._imageComponent.sprite = _startSprite;
			}
		}
		public void SoundEnabler()
		{
			_soundTogle = !_soundTogle;
			if (_soundTogle)
			{
				_soundButton.GetComponent<Image>().sprite = _tickImage;
			}
			else
			{
				_soundButton.GetComponent<Image>().sprite = _soundButtonImage;
			}
		}
		public void SetCoolDown()
		{
			_buttonCoolDown = 0;
		}
		#endregion
	}
}
