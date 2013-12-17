using UnityEngine;
using System.Collections;

public class ButtonClicked : MonoBehaviour {

	public TweenPosition tween;

	public Vector3 initialPosition;

	public GameObject targetObject;

	public int myData;

	public ButtonManager manager;

	// Use this for initializationth
	void Start () {

		initialPosition = targetObject.transform.localPosition;
		Debug.Log("yo "+targetObject.transform.localPosition);
		Debug.Log("yo "+initialPosition);

	}


	// Update is called once per frame
	void Update () {
	
	}

	public int clickCount = 0;

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);


		clickCount ++;
		//if(other.name.Contains("Rumbo"))
		if(clickCount==1)
		{

			Debug.Log("Trigger Rumbo");
		
			tween.from = initialPosition;
			tween.to = initialPosition + new Vector3(0,-0.12f,0);
			tween.Reset();
			tween.enabled = true;

			//get global object
			GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
			GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

			//sync puzzle 1 data
			if(globalSyncObject != null) globalSyncObject.triggerPuzzle1Input(myData);
			//manager.Code(myData);
		}

	}


	void OnTriggerExit(Collider other) {
		//Destroy(other.gameObject);

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
	}
}
