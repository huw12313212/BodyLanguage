using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSyncData : MonoBehaviour {

	public int puzzle1AnswerSize;
	public List<int> puzzle1Answer;

	// Use this for initialization
	void Start () {
		Debug.Log("Start!");
		puzzle1AnswerSize = 3;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		int syncTest = 0;
		
		if (stream.isWriting)
		{
			//Debug.Log("Wirting! test = "+test);
			//syncTest = test;
			stream.Serialize(ref syncTest);			
		}
		else
		{
			//Debug.Log("Received! test = "+test);
			stream.Serialize(ref syncTest);
			//test = syncTest;
		}
	}

	public void setPuzzle1Answer(List<int> answer)
	{
		if(answer!=null) puzzle1Answer = answer;
	}

}
