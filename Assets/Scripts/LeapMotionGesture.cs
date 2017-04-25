using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class LeapMotionGesture : MonoBehaviour {
	protected Controller leap_controller_;

	[SerializeField]
	private GameObject controlPanel;

	/** Creates a new Leap Controller object. */
	void Awake() {
		leap_controller_ = new Controller();
		Debug.Log("LeapMotionGesture is awake");
	}
	// Use this for initialization
	void Start () {
		if (leap_controller_ == null) {
			Debug.LogWarning(
				"Cannot connect to controller. Make sure you have Leap Motion v2.0+ installed");
		}
		//Enable gesture
		enableAllGestures();
		controlPanel.SetActive (false);
	}
		
	// Update is called once per frame
	void Update () {
		detectGestures (leap_controller_.Frame());
	}

	public void enableAllGestures()
	{
		Debug.Log("All gestures have been enabled.");
		leap_controller_.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		leap_controller_.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
		leap_controller_.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		leap_controller_.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
	}

	public void detectGestures(Leap.Frame frame)
	{
		GestureList gestures = frame.Gestures ();//return a list of gestures 
		for (int i = 0; i < gestures.Count; i++) //enumerate all the gestures detected in a frame
		{
			Gesture gesture = gestures [i];

			switch (gesture.Type) 
			{
			case Gesture.GestureType.TYPE_CIRCLE:
				Debug.Log ("Detect gesture: CIRCLE");
				CircleGesture circle = new CircleGesture (gesture);
				Debug.Log ("circle.Normal is:" + circle.Normal);
				//clockwise: open control panel
				if (circle.Normal.x < 0) {
					Debug.Log ("Circle position in Leap:"+circle.Center+" Circle position in Unity:"+circle.Center.ToUnity ());
					Debug.Log ("Move Control Panel from"+controlPanel.transform.position+"to"+circle.Center.ToUnity ());
					//controlPanel.transform.position = Vector3.MoveTowards (controlPanel.transform.position, circle.Center.ToUnity (),1);
					controlPanel.SetActive (true);
					Debug.Log ("Show Control Panel");
				};
				//counterclockwise:close control panel
				if(circle.Normal.x >= 0)
				{
					controlPanel.SetActive (false);
					Debug.Log ("Hide Control Panel");
				};
				break;
			case Gesture.GestureType.TYPE_KEY_TAP:
				Debug.Log("Detect gesture: KEY_TAP");
				break;
			case Gesture.GestureType.TYPE_SCREEN_TAP:
				Debug.Log ("Detect gesture: SCREEN_TAP");
				break;
			case Gesture.GestureType.TYPE_SWIPE:
				Debug.Log("Detect gesture: SWIPE");
				break;
			}
		}
	}





}
