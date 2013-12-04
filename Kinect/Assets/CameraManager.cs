using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	
	public GameObject Player1;
	public GameObject Player2;
	
	public float threashHold = 0.3f;
	
	public float speed = 0.01f;
	
	
	//Camera camera;
	// Use this for initialization
	void Start () {
		
		//camera = this.gameObject.GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 center = (Player1.transform.position + Player2.transform.position) /2;
		
		Vector3 PlayerDif = Player1.transform.position - Player2.transform.position;
		
		
	    Vector3 dif = center - transform.position;
		dif.z = 0;
		
		if(dif.magnitude > threashHold)
		{
			transform.position +=  speed * dif.normalized;
		}
		
		
		
		if(PlayerDif.magnitude/2 > camera.orthographicSize - 0.1f)
		{
			//camera.orthographicSize += speed;
		}
	
		
				
		if(PlayerDif.magnitude/2 < camera.orthographicSize + 0.1f)
		{
			//camera.orthographicSize -= speed;
		}
	}
}
