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

				clickCount ++;

				//rotate game object
				tween.from = gameObject.transform.rotation.eulerAngles;
				tween.to = initialRotation + (new Vector3((addRotation*clickCount)%360,90,90));
				tween.Reset();
				tween.enabled = true;
				
				//send data
				//*** Have Bug
				manager.Code(myIndex, Mathf.FloorToInt(gameObject.transform.rotation.eulerAngles.x/addRotation));
				
			}
		}

	}
	void OnTriggerEnter(Collider other) {

		isTrigger = true;
	}
	
	
	void OnTriggerExit(Collider other) {

		isTrigger = false;

	}
}
