using UnityEngine;
using System.Collections;

public class Puzzle3InputTextScript : MonoBehaviour {
	private bool isTrigger;
	private bool ButtonB = false;
	public GameObject inputTextGroup;
	public TextMesh textMesh;

	public GameObject colli;
	// Use this for initialization
	void Start () {
		isTrigger = false;
	}

	bool PreviousWii = false;

	// Update is called once per frame
	void Update () {

		//update rotation


		if (isTrigger){	

			//get button down
			ButtonB =  Input.GetButtonDown("ButtonB")||(!PreviousWii&&CameraManager.CurrentPlayer1Controller.wiimoteGetButtonA());
			//click button b
			PreviousWii = CameraManager.CurrentPlayer1Controller.wiimoteGetButtonA();

			if (ButtonB){
				//get text manager
				InputTextManager textManager = inputTextGroup.GetComponent<InputTextManager>();

				//add text 
				//get global object
				GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
				GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

				//sync puzzle 3 data
				if(globalSyncObject != null) globalSyncObject.triggerPuzzle3Input(textMesh.text);

				if(colli != null)
					colli.GetComponent<ShowPressB>().hideButtonB();
				//textManager.Code(textMesh.text);
			}
			/*else{
				if(colli != null)
					colli.GetComponent<ShowPressB>().showButtonB();
			}*/
		}
	}

	//trigger enter
	void OnTriggerEnter(Collider other) {

		
		//trigger the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			other.gameObject.GetComponent<ShowPressB> ().showButtonB ();
			isTrigger = true;
			colli = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		

		
		//hide the button B on the player
		if (other.gameObject.tag == "Player" && other.gameObject.networkView.isMine) {
			other.gameObject.GetComponent<ShowPressB> ().hideButtonB ();
			isTrigger = false;
			colli = null;
		}
		
	}

}
