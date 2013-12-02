using UnityEngine;
using System.Collections;

public class HelloWorld1 : MonoBehaviour {

	public GameObject player;
	
	public float speed = 0.0f;
	
	// Use this for initialization
	void Start () {
		Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update () {
		//	Debug.Log("Updates");
		
		bool ButtonA = Input.GetButton("ButtonA");
		bool ButtonB = Input.GetButton("ButtonB");
		bool ButtonX = Input.GetButton("ButtonX");
		bool ButtonY = Input.GetButton("ButtonY");		
		bool ButtonBack = Input.GetButton("ButtonBack");	
		bool ButtonStart = Input.GetButton("ButtonStart");	
		bool ButtonSholderLeft = Input.GetButton("ButtonSholderLeft");	
		bool ButtonSholderRight = Input.GetButton("ButtonSholderRight");	
		bool ButtonJoystickLeft = Input.GetButton("ButtonJoystickLeft");	
		bool ButtonJoystickRight = Input.GetButton("ButtonJoystickRight");	
		
        float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		
		float ArrowHorizontal = Input.GetAxis("ArrowHorizontal");
		float ArrowVertical = Input.GetAxis("ArrowVertical");
		float JoystickRightHorizontal = Input.GetAxis("JoystickRightHorizontal");
		float JoystickRightVertical = Input.GetAxis("JoystickRightVertical");
		
		float Triggers = Input.GetAxis("Triggers");
		
		

	  //player.rigidbody.velocity = new Vector3(-x*1.0f,0,-y*1.0f);
	
		/*if (ButtonA != false)
			Debug.Log("ButtonA is " + ButtonA);
		if (ButtonB != false)
			Debug.Log("ButtonB is " + ButtonB);
		if (ButtonX != false)
			Debug.Log("ButtonX is " + ButtonX);
		if (ButtonY != false)
			Debug.Log("ButtonY is " + ButtonY);
		if (ButtonBack != false)
			Debug.Log("ButtonBack is " + ButtonBack);
		if (ButtonStart != false)
			Debug.Log("ButtonStart is " + ButtonStart);
		if (ButtonSholderLeft != false)
			Debug.Log("ButtonSholderLeft is " + ButtonSholderLeft);
		if (ButtonSholderRight != false)
			Debug.Log("ButtonSholderRight is " + ButtonSholderRight);
		if (ButtonJoystickLeft != false)
			Debug.Log("ButtonJoystickLeft is " + ButtonJoystickLeft);
		if (ButtonJoystickRight != false)
			Debug.Log("ButtonJoystickRight is " + ButtonJoystickRight);
		
		if (ArrowHorizontal != 0)
			Debug.Log("ArrowHorizontal is " + ArrowHorizontal);
		if (ArrowVertical != 0)
			Debug.Log("ArrowVertical is " + ArrowVertical);
		if (JoystickRightHorizontal != 0)
			Debug.Log("JoystickRightHorizontal is " + JoystickRightHorizontal);
		if (JoystickRightVertical != 0)
			Debug.Log("JoystickRightVertical is " + JoystickRightVertical);
		
		if (Triggers != 0)
			Debug.Log("Triggers is " + Triggers);
		
	//	transform.Translate(x,y,0.0f);
		//Debug.Log(x + "  hi  " + y + "  hi  " + ButtonA);*/
	}
}
