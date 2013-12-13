using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class createPuzzle1Answer : MonoBehaviour {
	public NetworkView networkView;

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		
		if(other.tag == "Player")
		{
			Debug.Log("Trigger Item");



			//Destroy(gameObject);
		}
		
	}
}
