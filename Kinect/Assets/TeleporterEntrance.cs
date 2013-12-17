using UnityEngine;
using System.Collections;

public class TeleporterEntrance : MonoBehaviour {

	public ParticleSystem explosion;
	public GameObject exit;
	public GameObject DoorEffect;

	public bool Played = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {


		if(!Played)
		{
			Played = true;
			explosion.Play();
			DoorEffect.SetActive(false);
		if (other.gameObject == CameraManager.CurrentPlayer1)
		{
			CameraManager.CurrentPlayer1.transform.position = exit.transform.position;

		}
		}
	}
}
