using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour {

	public GameObject AnchorA;
	public GameObject Anchor;

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

		bool ButtonB = Input.GetButton("ButtonB");
		if (isTrigger){	


			if (ButtonB){
				//Debug.Log("ButtonB");
				activeA = true;
				//active = true;
			
			}
			else{
				activeA = false;
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



	}


	void OnTriggerExit(Collider other) {
		isTrigger = false;
		activeA = false;
		//active = false;

		//hide button B
		other.gameObject.GetComponent<ShowPressB>().hideButtonB();
	}
}
