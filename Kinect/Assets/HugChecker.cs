﻿using UnityEngine;
using System.Collections;

public class HugChecker : MonoBehaviour {


	public CameraManager cameraManager;

	public TreeGrowing grow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void OnTriggerEnter(Collider other) {

		touchCount++;

	}
	
	void OnTriggerExit(Collider other) {
		touchCount--;
	}

	public int touchCount = 0;

	void Update () {
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
				
				if(nodeManager1.HugTime > 3&&nodeManager2.HugTime>3)
				{
					grow.grow = true;
				}

			}
		}
	}
}
