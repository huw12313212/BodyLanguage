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


		if(networkView.isMine)
		{

			Debug.Log("Create Random Num");
			List<int> randomAnswer = new List<int>();
			for(int i = 0;i<puzzle1AnswerSize;i++)
			{
			randomAnswer.Add(Random.Range(0,puzzle1AnswerSize));
			}
			
			networkView.RPC("setPuzzle1RPC",RPCMode.AllBuffered,randomAnswer[0],randomAnswer[1],randomAnswer[2]);
		}

	}

	void Update()
	{
		if(puzzle1Answer.Count!=0) Debug.Log("Puzzle 1 Answer = "+puzzle1Answer[0]+" "+puzzle1Answer[1]+" "+puzzle1Answer[2]);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		List<int> syncTemp;
		
		if (stream.isWriting)
		{
			Debug.Log("Wirting!");
			//syncTemp = puzzle1Answer;

			/*
			for(int i =0;i<syncTemp.Count;i++)
			{
				stream.Serialize(ref syncTemp[i]);
			}
			*/
			//stream.Serialize(ref syncTemp);			
		}
		else
		{
			Debug.Log("Received!");
			//stream.Serialize(ref syncTemp);

			/*
			for(int i =0;i<syncTemp.Count;i++)
			{
				syncTemp[i] = puzzle1Answer[i];
			}
			*/
			//syncTemp = puzzle1Answer;
		}
	}

	public void setPuzzle1Answer(List<int> answer)
	{
		if(answer!=null) puzzle1Answer = answer;
	}

	[RPC]
	void setPuzzle1RPC(int i1,int i2,int i3)
	{
		//if(answer!=null) puzzle1Answer = answer;


		puzzle1Answer.Add(i1);
		puzzle1Answer.Add(i2);
		puzzle1Answer.Add(i3);
	}
}
