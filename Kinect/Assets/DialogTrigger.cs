using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour {

	public GameObject AnchorA;
	public GameObject Anchor;

	public GameObject colli;

	public bool activeA = false;
	//public bool active = false;
	public bool isTrigger = false;
	public string playerName = "carl";
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		AnchorA.SetActive(activeA);
		//Anchor.SetActive(active);

		//Debug.Log(activeA +""+ active +""+ isTrigger);

		if(CameraManager.CurrentPlayer1Controller == null)return;

		bool ButtonB = Input.GetButton("ButtonB") || CameraManager.CurrentPlayer1Controller.wiimoteGetButtonA();
		if (isTrigger){	


			if (ButtonB){
				//Debug.Log("ButtonB");
				activeA = true;
				if(colli != null)
					colli.GetComponent<ShowPressB>().hideButtonB();
				//active = true;
			
			}
			else{
				activeA = false;
				if(colli != null)
					colli.GetComponent<ShowPressB>().showButtonB();
				//active = false;

			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);

		Debug.Log("name:"+other.name);
		isTrigger = true;

		//trigger button B
		other.gameObject.GetComponent<ShowPressB>().showButtonB();
		colli = other.gameObject;


	}


	void OnTriggerExit(Collider other) {
		isTrigger = false;
		activeA = false;
		//active = false;

		//hide button B
		other.gameObject.GetComponent<ShowPressB>().hideButtonB();
		colli = null;
	}
}
