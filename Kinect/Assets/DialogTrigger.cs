using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour {

	public GameObject AnchorA;
	public GameObject Anchor;


	public bool activeA = false;
	public bool active = false;
	public bool isTrigger = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		AnchorA.SetActive(activeA);
		Anchor.SetActive(active);

		//Debug.Log(activeA +""+ active +""+ isTrigger);

		bool ButtonB = Input.GetButton("ButtonB");
		if (isTrigger){	
			if (ButtonB){
				//Debug.Log("ButtonB");
				activeA = false;
				active = true;
			
			}
			else{
				activeA = true;
				active = false;

			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);

		Debug.Log("name:"+other.name);
		isTrigger = true;


	}


	void OnTriggerExit(Collider other) {
		isTrigger = false;
		activeA = false;
		active = false;
	}
}
