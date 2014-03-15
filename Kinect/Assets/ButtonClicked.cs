using UnityEngine;
using System.Collections;

public class ButtonClicked : MonoBehaviour {

	public TweenPosition tween;

	public Vector3 initialPosition;

	public GameObject targetObject;

	public int myData;

	public ButtonManager manager;
	private bool isClicked = false;
	private bool ButtonA = false;
	private bool previousWiiA = false;

	public MusicHandler musicHandler;
	// Use this for initializationth
	void Start () {

		initialPosition = targetObject.transform.localPosition;

		//set tween finish function
		tween.onFinished = onButtonClickFinish;
	}


	// Update is called once per frame
	void Update () {
	
	}

	public int clickCount = 0;

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);

		/*
		clickCount ++;
		
		if(clickCount==1)
		{
			
			Debug.Log("Trigger Rumbo");
			
			tween.from = initialPosition;
			tween.to = initialPosition + new Vector3(0,-0.12f,0);
			tween.Reset();
			tween.enabled = true;
			
			if(other.networkView.isMine){
				//get global object
				GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
				GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();
				
				//sync puzzle 1 data
				if(globalSyncObject != null) globalSyncObject.triggerPuzzle1Input(myData);
				//manager.Code(myData);
			}
		}
		*/

		//get show press
		if((other.gameObject.tag == "Player")){
			ShowPressB showPress = other.gameObject.GetComponent<ShowPressB> ();
			if(showPress!=null) showPress.showButtonB();
		}
	}

	void OnTriggerStay(Collider other) {

		//get button down
		ButtonA =  (Input.GetButtonDown("ButtonA")||(!previousWiiA&&CameraManager.CurrentPlayer1Controller.wiimoteGetButtonA()));
		previousWiiA = CameraManager.CurrentPlayer1Controller.wiimoteGetButtonA();

		//trigger button click
		if((other.gameObject.tag == "Player") && (ButtonA)) buttonClick(other);

	}

	void OnTriggerExit(Collider other) {
		//Destroy(other.gameObject);

		/*
		clickCount --;
		//if(other.name.Contains("Rumbo"))
		if(clickCount == 0)
		{
			Debug.Log("Exit Rumbo");

			tween.from = targetObject.transform.localPosition;
			tween.to = initialPosition;
			tween.Reset();
			tween.enabled = true;
		}
		*/

		//hide press
		if((other.gameObject.tag == "Player")){
			ShowPressB showPress = other.gameObject.GetComponent<ShowPressB> ();
			if(showPress!=null) showPress.hideButtonB();
		}
	}

	void buttonClick(Collider other)
	{
		//clickCount ++;
		
		if(!isClicked)
		{
			isClicked = true;

			//play sound
			if(musicHandler!=null) musicHandler.playButtonClickAudio();

			//Debug.Log("Trigger Rumbo");
			
			tween.from = initialPosition;
			tween.to = initialPosition + new Vector3(0,-0.15f,0);
			tween.Reset();
			tween.enabled = true;

			if(other.networkView.isMine){
				//get global object
				GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
				GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

				//sync puzzle 1 data
				if(globalSyncObject != null) globalSyncObject.triggerPuzzle1Input(myData);

			}
		}
	}


	void onButtonClickFinish(UITweener tweener){
		//Debug.Log("Exit Rumbo");
		
		tween.from = targetObject.transform.localPosition;
		tween.to = initialPosition;
		tween.Reset();
		tween.enabled = true;

		isClicked = false;
	}
}
