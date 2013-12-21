using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeGrowing : MonoBehaviour {
	public List<GameObject> particleSystemList;
	public TweenScale tween;
	private bool isTrigger;
	public GameObject mainCamera;
	public bool grow
	{
		set
		{
			tween.enabled  = value;

			for(int i = 0;i<particleSystemList.Count;i++)
			{
				particleSystemList[i].SetActive(true);
			}
		}
	}

	// Use this for initialization
	void Start () {
		isTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		//check in collider or not
		if(isTrigger == true)
		{
			CameraManager cameraManager = mainCamera.GetComponent<CameraManager>();
			GameObject player1 = cameraManager.Player1;
			GameObject player2 = cameraManager.Player2;

			//check player
			if((player1 != null) && (player2 != null))
			//if((player1 != null))
			{
				NodeManager nodeManager1 = player1.GetComponent<NodeManager>();
				NodeManager nodeManager2 = player2.GetComponent<NodeManager>();

				//check 
				if((nodeManager1 != null) && (nodeManager2 != null))
				//if((nodeManager1 != null))
				{
					//get player 1 bone data
					for(int i = 0;i<nodeManager1.dataList.Count;i++)
					{
						//Debug.Log("Data["+i+"] :"+nodeManager1.dataList[i].syncRotation);
					}

					//get player 2 bone data
					for(int i = 0;i<nodeManager2.dataList.Count;i++)
					{
						//Debug.Log("Data["+i+"] :"+nodeManager2.dataList[i].syncRotation);
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//test
		grow = true;
		//trigger flag
		isTrigger = true;
	}

	void OnTriggerExit(Collider other) {
		isTrigger = false;
	}
}
