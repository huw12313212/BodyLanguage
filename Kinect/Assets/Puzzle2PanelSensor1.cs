using UnityEngine;
using System.Collections;

public class Puzzle2PanelSensor1 : MonoBehaviour {

	public TweenRotation tween;
	private Vector3 initialRotation;
	public int myIndex;
	public Puzzle2Manager manager;
	public int partationNum;
	private bool isTrigger;
	private int clickCount = 0;
	private bool ButtonB = false;
	private float addRotation;
	public float initialRotationOffset;

	// Use this for initializationth
	void Start () {
		//initial position
		initialRotation = gameObject.transform.rotation.eulerAngles;
		isTrigger = false;
		addRotation = 360/partationNum;

	}

	// Update is called once per frame
	void Update () {

		if (isTrigger){	

			//get button down
			ButtonB =  Input.GetButtonDown("ButtonB");
			//click button b

			if (ButtonB){
				//click count
				clickCount ++;

				//rotate game object
				float rotationOffest = (addRotation*clickCount)%360;
				tween.from = gameObject.transform.rotation.eulerAngles;
				tween.to = initialRotation + (new Vector3(rotationOffest,90,90));
				tween.Reset();
				tween.enabled = true;
				
				//send data
				//*** Have Bug
				Debug.Log ("Answer = "+Mathf.FloorToInt((rotationOffest)/addRotation));
				manager.Code(myIndex, Mathf.FloorToInt((rotationOffest)/addRotation));
				
			}
		}

	}
	void OnTriggerEnter(Collider other) {
		isTrigger = true;

		//trigger the button B on the player
		if(other.gameObject.tag == "Player")
			other.gameObject.GetComponent<ShowPressB>().showButtonB();

	}
	
	
	void OnTriggerExit(Collider other) {

		isTrigger = false;

		//hide the button B on the player
		if(other.gameObject.tag == "Player")
			other.gameObject.GetComponent<ShowPressB> ().hideButtonB ();

	}
}
