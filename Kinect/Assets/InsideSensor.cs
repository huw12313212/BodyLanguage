using UnityEngine;
using System.Collections;

public class InsideSensor : MonoBehaviour {

	public TweenPosition closeDoorTween;

	public int numberOfPlayer;

	public int currentNumber = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);

		Debug.Log("Player Enter");

		currentNumber++;

		if(currentNumber == numberOfPlayer)
		{
			closeDoorTween.enabled = true;
		}
	}

	void OnTriggerExit(Collider other) {
		//Destroy(other.gameObject);

		currentNumber--;


	}
}
