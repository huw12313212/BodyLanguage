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

			//check no one on top
			GameObject[] objectArray = GameObject.FindGameObjectsWithTag("Player");
			bool noOneOnTop = true;
			foreach(GameObject player in objectArray)
			{
				BotControlScript script = player.GetComponent<BotControlScript>();
				if(script!=null)
				{
					Debug.Log("onTop = "+script.onTop);
					if(script.onTop == true) noOneOnTop = false;
				}
			}

			if(noOneOnTop == true)
			{
				if (other.gameObject == CameraManager.CurrentPlayer1)
				{
					//set flag
					BotControlScript script = CameraManager.CurrentPlayer1.GetComponent<BotControlScript>();
					if(script!=null) script.onTop = true;

					//position
					CameraManager.CurrentPlayer1.transform.position = exit.transform.position;

				}
			}

		}
	}
}
