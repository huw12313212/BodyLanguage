using UnityEngine;
using System.Collections;

public class Puzzle2PanelSensor1 : MonoBehaviour {
	
	public TweenRotation tween;
	public Vector3 initialRotation;
	public int myIndex;
	public Puzzle2Manager manager;
	public int partationNum;
	public bool isTrigger;
	public int clickCount = 0;
	public bool ButtonB = false;
	public float addRotation;
	public int order = -1;
	public GameObject globalSync = null;
	// Use this for initializationth
	void Start () {
		//initial position
		initialRotation = gameObject.transform.rotation.eulerAngles;
		isTrigger = false;
		addRotation = 360/partationNum;
		globalSync = GameObject.Find ("GlobalSyncData");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (globalSync == null) {
			globalSync = GameObject.Find ("GlobalSyncData");
		} else
			Debug.Log ("nul!!");
		
		if (isTrigger){	
			
			//get button down
			ButtonB =  Input.GetButtonDown("ButtonB");
			//click button b
			
			if (ButtonB){
				//click count
				clickCount ++;
				
				//rotate game object
				detectNumber();
				if(order != -1){
					
					order = -1;
				}else
					Debug.Log("No order-----------------------------------------------");
				
				float rotationOffest = (addRotation*clickCount)%360;
				//tween.from = gameObject.transform.rotation.eulerAngles;
				//tween.to = initialRotation + (new Vector3(0,0,rotationOffest));
				//tween.Reset();
				//tween.enabled = true;
				
				//send data
				//*** Have Bug
				Debug.Log ("Answer = "+Mathf.FloorToInt((rotationOffest)/addRotation));
				manager.Code(myIndex, Mathf.FloorToInt((rotationOffest)/addRotation));
				
			}
		}
		
	}
	void OnTriggerEnter(Collider other) {

		
		//trigger the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			other.gameObject.GetComponent<ShowPressB> ().showButtonB ();
			isTrigger = true;
		}
		
	}
	
	
	void OnTriggerExit(Collider other) {
		

		
		//hide the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			isTrigger = false;
			other.gameObject.GetComponent<ShowPressB> ().hideButtonB ();
		}
		
	}
	void detectNumber(){
		if (gameObject.name == "Puzzle 2 Sensor 1") {
			order = 1;
		} else if (gameObject.name == "Puzzle 2 Sensor 2") {
			order = 2;
		} else if (gameObject.name == "Puzzle 2 Sensor 3") {
			order = 3;
		}
	}
}
