using UnityEngine;
using System.Collections;

public class ShowPressB : MonoBehaviour {
	public GameObject buttonB;
	// Use this for initialization
	void Start () {
		buttonB.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showButtonB(){
		buttonB.SetActive (true);
	}
	public void hideButtonB(){
		buttonB.SetActive (false);

	}
}
