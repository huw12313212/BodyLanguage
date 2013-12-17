using UnityEngine;
using System.Collections;

public class Puzzle3InputTextScript : MonoBehaviour {
	private bool isTrigger;
	private bool ButtonB = false;
	public GameObject inputTextGroup;
	public TextMesh textMesh;
	// Use this for initialization
	void Start () {
		isTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {

		//update rotation


		if (isTrigger){	
			
			//get button down
			ButtonB =  Input.GetButtonDown("ButtonB");
			//click button b
			
			if (ButtonB){
				//get text manager
				InputTextManager textManager = inputTextGroup.GetComponent<InputTextManager>();

				//add text 
				//get global object
				GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
				GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

				//sync puzzle 3 data
				if(globalSyncObject != null) globalSyncObject.triggerPuzzle3Input(textMesh.text);

				//textManager.Code(textMesh.text);
			}
		}
	}

	//trigger enter
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
