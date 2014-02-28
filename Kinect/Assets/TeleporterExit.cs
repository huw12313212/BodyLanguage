using UnityEngine;
using System.Collections;

public class TeleporterExit : MonoBehaviour {


	public GameObject teleporterEntrance;
	public bool played = false;
	public ParticleSystem explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if((!played))
		{
			played = true;
			explosion.Play();	
			teleporterEntrance.SetActive(false);
		}
	}
}
