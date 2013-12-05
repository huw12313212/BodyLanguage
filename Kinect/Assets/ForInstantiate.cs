using UnityEngine;
using System.Collections;

public class ForInstantiate : MonoBehaviour {
	
	
	
	public KinectManager kinectManager;
	
	// Use this for initialization
	void Start () {
		
		KinectManager[] avatars = FindObjectsOfType(typeof(KinectManager)) as KinectManager[];
		
		kinectManager = avatars[0];
		
		kinectManager.Player1Controllers.Add(GetComponent<AvatarController>());
		
		
	
	}
	
	int i = 0;
	
	void Update()
	{
		
		i++;
		
		if(i==30)
		{
			GetComponent<BotControlScript>().enabled = true;
			
			//Debug.Log("helllllllllll");
		}
		
	}
	

}
