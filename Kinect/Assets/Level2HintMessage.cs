using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2HintMessage : MonoBehaviour {


	public List<UISprite> Sprites1;
	public List<UISprite> Sprites2;
	public List<UISprite> Sprites3;

	public List<List<UISprite>> SpritesList;

	public List<int> Answer;

	void Awake ()
	{
		GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
		
		Answer = sync.puzzle2Answer;

		SpritesList = new List<List<UISprite>>();
		SpritesList.Add(Sprites1);
		SpritesList.Add(Sprites2);
		SpritesList.Add(Sprites3);


		for(int i = 0 ; i < SpritesList.Count; i++)
		{
			List<UISprite> currentSprites = SpritesList[i];

			for(int j = 0 ; j<currentSprites.Count ; j++)
			{
				UISprite currentSprite = currentSprites[j];

				if(j == Answer[i])
				{
					currentSprite.enabled = true;
				}
				else
				{
					currentSprite.enabled = false;
				}
			}
		}

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
