using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	
	public GameObject Player1;
	public GameObject Player2;

	public static GameObject CurrentPlayer1;
	public static BotControlScript CurrentPlayer1Controller;

	public float threashHold = 0.3f;
	
	public float speed = 0f;
	public float sizeChangeSpeed = 0f;
	public float YOffset = 0f;
	
	
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
		
		
		//transform.position.y += camera.orthographicSize *YOffset;
		
		transform.position +=  speed * dif * Time.deltaTime;
		
		transform.position = new Vector3(transform.position.x,YOffset*camera.orthographicSize,transform.position.z);

		if(PlayerDif.magnitude/2 < camera.orthographicSize + 0.5f)
		{
				float difRatio = camera.orthographicSize + 0.1f -PlayerDif.magnitude/2;
				
				camera.orthographicSize -= sizeChangeSpeed* Time.deltaTime *difRatio;


		}
			
	
			
	
		//if(camera.orthographicSize>4)
		//{
			
			if(PlayerDif.magnitude/2 > camera.orthographicSize - 0.5f)
			{
				
				float difRatio = PlayerDif.magnitude/2 - camera.orthographicSize - 0.1f;
				
				camera.orthographicSize += sizeChangeSpeed* Time.deltaTime * difRatio;

			}
			
				//}
		
		
			if(camera.orthographicSize<4)camera.orthographicSize = 4;
			
	
		float ratio = (float)camera.orthographicSize;
		camera.transform.localScale = new Vector3(ratio,ratio,1);
		
		

	
		
				

	}
}
