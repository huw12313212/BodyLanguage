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
		List<int> syncTemp = new List<int>();
		
		if (stream.isWriting)
		{
			Debug.Log("Wirting!");
			syncTemp = puzzle1Answer;
			stream.Serialize(ref syncTemp);			
		}
		else
		{
			Debug.Log("Received!");
			stream.Serialize(ref syncTemp);
			syncTemp = puzzle1Answer;
		}
	}

	public void setPuzzle1Answer(List<int> answer)
	{
		if(answer!=null) puzzle1Answer = answer;
	}

}
