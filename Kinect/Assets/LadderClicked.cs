using UnityEngine;
using System.Collections;

public class LadderClicked : MonoBehaviour {
	
	public TweenPosition tween;
	
	public GameObject ladderSensor;
	public GameObject ladder;
	public bool onLadder;
	public CameraManager cameraManager;
	public bool isPlayer1;
	public bool isPlayer2;
	
	public Vector3 initialPosition;
	
	public GameObject targetObject;
	
	public int myData;
	
	public ButtonManager manager;
	
	// Use this for initializationth
	void Start () {
		
		initialPosition = targetObject.transform.localPosition;
		Debug.Log("yo "+targetObject.transform.localPosition);
		Debug.Log("yo "+initialPosition);
		onLadder = false;
		isPlayer1 = false;
		isPlayer2 = false;
	}
	
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(onLadder);
		if (onLadder)
		{
			//Debug.Log("hi");
			if (ladder.transform.position.y < 0.1)
			{
				if (isPlayer1)
				{
					Debug.Log("isPlayer1");
					ladder.transform.position +=new Vector3(0,0.1f,0);
					cameraManager.Player1.rigidbody.useGravity = false;
					cameraManager.Player1.transform.position += new Vector3(0,0.1f,0);

				}
				if (isPlayer2)
				{
					Debug.Log("isPlayer2");
					ladder.transform.position +=new Vector3(0,0.1f,0);
					cameraManager.Player2.rigidbody.useGravity = false;
					cameraManager.Player2.transform.position += new Vector3(0,0.1f,0);

				}
				//if (ladder.transform.position.y>10)
				//	ladderSensor.SetActive(false);
			}
		}
	}
	
	public int clickCount = 0;
	
	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		
		onLadder = true;
		if (other.gameObject == cameraManager.Player1)
			isPlayer1 = true;
		if (other.gameObject == cameraManager.Player2)
			isPlayer2 = true;
		
		/*
		clickCount ++;
		//if(other.name.Contains("Rumbo"))
		if(clickCount==1)
		{

			Debug.Log("Trigger Rumbo");
			Debug.Log("hi" + onLadder);
			onLadder = true;
			tween.from = initialPosition;
			tween.to = initialPosition + new Vector3(0,-0.12f,0);
			tween.Reset();
			tween.enabled = true;

			manager.Code(myData);

		}*/
		
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
	}
}
