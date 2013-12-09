using UnityEngine;
using System.Collections;

public class ForInstantiate : MonoBehaviour {
	
	
	
	public KinectManager kinectManager;
	
	// Use this for initialization
	void Start () {
		
		KinectManager[] avatars = FindObjectsOfType(typeof(KinectManager)) as KinectManager[];
		
		kinectManager = avatars[0];
		
		
		if(networkView.isMine)
			kinectManager.Player1Controllers.Add(GetComponent<AvatarController>());
		
		
	
	}
	
	int i = 0;
	
	void Update()
	{
		
		i++;
		
		if(i==30)
		{
			BotControlScript script = GetComponent<BotControlScript>();
			
			if(networkView.isMine)
			{
				script.enabled = true;
				
			}
			else
			{
				script.enabled = true;
				gameObject.rigidbody.useGravity = false;//.useGravity = false;
			}
			
			//Debug.Log("helllllllllll");
		}
		
	}
	

}
