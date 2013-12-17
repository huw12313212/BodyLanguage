using UnityEngine;
using System.Collections;

public class Puzzle2PanelSensor : MonoBehaviour {
	
	public TweenRotation tween;
	public Vector3 initialRotation;
	public int myIndex;
	public Puzzle2Manager manager;
	public int partationNum;
	private bool isTrigger;
	public GameObject colli;
	//public int clickCount = 0;
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
				//click count
				//clickCount ++;
				Debug.Log ("Click Button B");
				int currentInput = (manager.inputCode[myIndex] + 1)%(partationNum);
				//float rotationOffest = (addRotation*currentInput)%360;
				//tween.from = gameObject.transform.rotation.eulerAngles;
				//tween.to = initialRotation + (new Vector3(0,0,rotationOffest));
				//tween.Reset();
				//tween.enabled = true;
				
				//send data
				Debug.Log ("Answer = "+Mathf.FloorToInt(currentInput));

				//get global object
				GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
				GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

				//sync puzzle 1 data
				if(globalSyncObject != null) globalSyncObject.triggerPuzzle2Input(myIndex,currentInput);


				if(colli != null){
					Debug.Log("Not nullllllllllllllll :"+colli.GetComponent<ShowPressB>());
					colli.GetComponent<ShowPressB>().hideButtonB();
				}

				//manager.Code(myIndex, Mathf.FloorToInt(currentInput));
				
			}
			/*lse{
				if(colli != null)
					colli.GetComponent<ShowPressB>().showButtonB();
			}*/
		}
		
	}

	void OnTriggerEnter(Collider other) {
		//trigger the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			Debug.Log("call call call");
			isTrigger = true;
			other.gameObject.GetComponent<ShowPressB> ().showButtonB ();

			colli = other.gameObject;
		}
		
	}
	
	
	void OnTriggerExit(Collider other) {
		//hide the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			isTrigger = false;
			other.gameObject.GetComponent<ShowPressB> ().hideButtonB ();
			colli = null;
		}
		
	}

	public void UpdatePanelRotation()
	{
		float rotationOffest = (addRotation*manager.inputCode[myIndex])%360;
		tween.from = gameObject.transform.rotation.eulerAngles;
		tween.to = initialRotation + (new Vector3(0,0,rotationOffest));
		tween.Reset();
		tween.enabled = true;
	}
}
