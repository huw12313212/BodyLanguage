using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {


	public Vector3 speed;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 dif = Time.deltaTime * speed;

		gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + speed);
	}
}
