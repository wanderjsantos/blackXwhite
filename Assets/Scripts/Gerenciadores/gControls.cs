using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gControls : MonoBehaviour {

	public float MinTouchDeltaToDetectDrag = 10f;
	public Vector3 NormalizedDelta;

	Vector3 currentDelta;
	bool checkMag = false;
	Vector3 lastPosition;
	public float deltaMagnitude	=	0f;
	public bool inputPressed = false;
	Vector3 inputPosition;

	float timeToDetectMultiple = 0.1f;

	public List<KeyboardCommands> keyboardList;
	public Dictionary<string, KeyboardCommands> keyboardCommandsDict = new Dictionary<string, KeyboardCommands>();

	public List<MouseOrTouchCommands> mouseList;
	public Dictionary<string, MouseOrTouchCommands> mouseCommandsDict = new Dictionary<string, MouseOrTouchCommands>();

	public static gControls s;
	void Awake()
	{
		s = this;

		int i = 0;
		keyboardCommandsDict = new Dictionary<string, KeyboardCommands>();
		for(i = 0; i < keyboardList.Count; i++)
		{
			keyboardCommandsDict.Add(keyboardList[i].name, keyboardList[i]);
		}

		mouseCommandsDict = new Dictionary<string, MouseOrTouchCommands>();
		for( i = 0; i < mouseList.Count; i++ )
		{
			mouseCommandsDict.Add(mouseList[i].name, mouseList[i]);
		}

	}

	void Update()
	{
#if UNITY_ANDROID || UNITY_IPHONE
		if( Input.touchCount <= 0 ) return;

		inputPressed = Input.GetTouch(0).phase == TouchPhase.Moved;
		inputPosition = Input.GetTouch(0).position;
		if( Input.GetTouch(0).phase == TouchPhase.Began ) lastPosition = inputPosition;
#else
//#if UNITY_EDITOR
		inputPressed = Input.GetMouseButton(0);
		inputPosition = Input.mousePosition;

		if( Input.GetMouseButtonDown(0)) lastPosition = inputPosition;

#endif
		if(inputPressed)
		{
			currentDelta = inputPosition - lastPosition;
			lastPosition = inputPosition;
			deltaMagnitude = currentDelta.magnitude;
			NormalizedDelta = currentDelta.normalized;
		}

	}

	public bool Dragging()
	{
		return deltaMagnitude > MinTouchDeltaToDetectDrag;
	}

	public bool DoCommand( string keyString )
	{
		if( !keyboardCommandsDict.ContainsKey(keyString) ) return false;

		return keyboardCommandsDict[keyString].isActive();
	}

	public bool DoMouseCommand( string keyString )
	{
		if(!mouseCommandsDict.ContainsKey(keyString) ||  Input.touchCount <= 0 ) return false;
		
		return mouseCommandsDict[keyString].isActive();
	}


}


[System.Serializable]
public class KeyboardCommands : iCommand
{
	public enum KeyboardAction{Down, Up, Pressed, None};

	public string name = "Key";

	public KeyboardAction action = KeyboardAction.None;
	public KeyCode code = KeyCode.None;

	public bool isActive()
	{
		switch(action)
		{
		case KeyboardAction.Down:
			return (Input.GetKeyDown(code));
		case KeyboardAction.Up:
			return (Input.GetKeyUp(code));
		case KeyboardAction.Pressed:
			return (Input.GetKey(code));
		default:
			return false;
			break;
		}
		return false;
	}
}

[RequireComponent(typeof(gControls))][System.Serializable]
public class MouseOrTouchCommands : iCommand
{
	public enum MouseOrTouchAction{ Down, Up, Pressed, Drag,  Multiple, Simultaneous, None };
	public string name = "Mouse/Touch";
	public MouseOrTouchAction action = MouseOrTouchAction.None;
	//if Mouse, must be 0 or 1
	public int mouseButton = 0;
	public int numberOfClicksOrTouches = 1;

	public bool isActive()
	{
		switch(action)
		{
		case MouseOrTouchAction.Down:
			return isDown();
		case MouseOrTouchAction.Pressed:
			return isPressed();
		case MouseOrTouchAction.Up:
			return isUp ();
		case MouseOrTouchAction.Drag:
			return isDrag();
		default:
			break;
		}
		return false;
	}

	//isDown
	bool isDown()
#if UNITY_ANDROID || UNITY_IPHONE
	{return Input.GetTouch(mouseButton).phase == TouchPhase.Began && gControls.s.deltaMagnitude == 0;}
#else
//#if UNITY_EDITOR && !UNITY_ANDROID
	{return Input.GetMouseButtonDown(mouseButton) && gControls.s.deltaMagnitude == 0;}
#endif
	//isPressed ?
	bool isPressed()
#if UNITY_ANDROID || UNITY_IPHONE
	{return Input.GetTouch(mouseButton).phase == TouchPhase.Moved;}
#else
//#if UNITY_EDITOR
	{return Input.GetMouseButton(mouseButton);}
#endif

	//isUp ?
	bool isUp()
#if UNITY_ANDROID || UNITY_IPHONE
	{return Input.GetTouch(mouseButton).phase == TouchPhase.Ended;}
#else
//#if UNITY_EDITOR
	{return Input.GetMouseButtonUp(mouseButton);}
#endif

	//is dragging ?
	bool isDrag()
	{return isPressed() && gControls.s.Dragging();}
}

public interface iCommand
{
	bool isActive();
}
