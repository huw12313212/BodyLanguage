using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LeaderBoardManager : MonoBehaviour {
	public List<GameObject> leaderLabelArray;
	public TweenScale scaleTween;
	public GameObject leaderboardUI;
	// Use this for initialization
	void Start () {
	
	}

	public void setData(JSONObject dataObject)
	{
		leaderboardUI.SetActive(true);
		scaleTween.enabled = true;
	
		//set data
		for(int i = 0;i <leaderLabelArray.Count;i++){

			UILabel label = leaderLabelArray[i].GetComponent<UILabel>();

			if(i>=dataObject.list.Count)
			{
				Debug.Log("1");
				label.text = " ";
			}
			else
			{
				Debug.Log("2");
				JSONObject itemData = dataObject[i];

				string name1 = itemData["username1"].str;
				string name2 = itemData["username2"].str;
				float time =  (float)itemData["exeTime"].n;

				TimeSpan timeSpan = TimeSpan.FromSeconds(time);

				string timeStr = string.Format("{0:00}:{1:00}:{2:00}s",timeSpan.Hours,timeSpan.Minutes, timeSpan.Seconds);

				label.text = " "+i+"."+name1 +" and "+name2+"\n                               "+timeStr;
			}

		}


	}

	// Update is called once per frame
	void Update () {
	
	}
}
