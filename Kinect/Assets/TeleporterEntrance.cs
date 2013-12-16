using UnityEngine;
using System.Collections;

public class TeleporterEntrance : MonoBehaviour {

	public ParticleSystem explosion;
	public GameObject exit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		explosion.Play();

		if (other.gameObject == CameraManager.CurrentPlayer1)
		{
			CameraManager.CurrentPlayer1.transform.position = exit.transform.position;

		}
	}
}
