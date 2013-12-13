using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageHint : MonoBehaviour {
	
	public List<int> message = new List<int>();
	public List<UISprite> Sprites = new List<UISprite>();
	
	// Use this for initialization
	void Start () {
	
		/*Labels[0].text = message[0] + "";
				Labels[1].text = message[1] + "";
				Labels[2].text = message[2] + "";
				Labels[3].text = message[3] + "";
		
		Labels[2].text = "Press A";
		*/
		
		
	}

	void Awake ()
	{

		GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();

		message = sync.puzzle1Answer;


		for(int i =0 ; i < message.Count ; i++)
		{
			for(int j = 0;j<3;j++)
			{
				int index = i*3 + j;

				if(message[i]==j)
				{
					Sprites[index].color = new Color(1,1,1);
				}
				else
				{
					Sprites[index].color = new Color(0.2f,0.2f,0.2f);
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
