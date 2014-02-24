using UnityEngine;
using System.Collections;

public class HugChecker : MonoBehaviour {


	public CameraManager cameraManager;

	public TreeGrowing grow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void OnTriggerEnter(Collider other) {


		if(other.gameObject.tag == "Player")
		{
		touchCount++;
		}

	}
	
	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Player")
		{
		touchCount--;
		}
	}

	public int touchCount = 0;

	void Update () {

		/* For single player test
		if(touchCount == 1)
		{
			GameObject player1 = cameraManager.Player1;
			if(player1 != null)
			{
				NodeManager nodeManager1 = player1.GetComponent<NodeManager>();

				if(nodeManager1.HugTime > 2.0f)
				{
					if(grow.grow != true)
					{
						GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
						sync.syncWorldTree();
					}
				}
				
			}
		}
		*/

		//Debug.Log("fuck....");
		if(touchCount == 2)
		{
			//CameraManager cameraManager = mainCamera.GetComponent<CameraManager>();
			GameObject player1 = cameraManager.Player1;
			GameObject player2 = cameraManager.Player2;
			
			//check player
			if((player1 != null) && (player2 != null))
				//if((player1 != null))
			{
				NodeManager nodeManager1 = player1.GetComponent<NodeManager>();
				NodeManager nodeManager2 = player2.GetComponent<NodeManager>();
				
				if(nodeManager1.HugTime > 2.0f && nodeManager2.HugTime > 2.0f)
				{
					if(grow.grow != true)
					{
						GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
						sync.syncWorldTree();
					}
				}

			}
		}
	}
	
}
