using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSyncData : MonoBehaviour {

	public int puzzle1AnswerSize;
	public int puzzle2AnswerSize;
	public int puzzle3AnswerSize;
	public List<int> puzzle1Answer;
	public List<int> puzzle2Answer;
	public List<string> puzzle3Answer;
	public List<string> puzzle3AnswerStringSet;

	// Use this for initialization
	void Start () {
		Debug.Log("Start!");
		//answer size
		puzzle1AnswerSize = 3;
		puzzle2AnswerSize = 3;
		puzzle3AnswerSize = 1;

		//init
		puzzle1Answer = new List<int>();
		puzzle2Answer = new List<int>();
		puzzle3Answer = new List<string>();

		//init puzzle answer
		if(networkView.isMine)
		{
			Debug.Log("Init Global Sync data");
			//puzzle 1
			Debug.Log("Create Random Num");
			List<int> randomAnswer = new List<int>();
			for(int i = 0;i<puzzle1AnswerSize;i++)
			{
				//random answer
				randomAnswer.Add(Random.Range(0,puzzle1AnswerSize));
			}

			//RPC sync data
			networkView.RPC("setPuzzle1RPC",RPCMode.AllBuffered,randomAnswer[0],randomAnswer[1],randomAnswer[2]);

			//puzzle 2

			//zero flag
			bool zeroFlag = true;
			//check all zero or not
			while(zeroFlag){
				//clear  random answer
				randomAnswer.Clear();

				//create random answer
				for(int i = 0;i<puzzle2AnswerSize;i++)
				{
					//range is [0,1] [0,2] [0,3]
					int randomNum = Random.Range(0,i+2);
					randomAnswer.Add(randomNum);
					if(randomNum!=0) zeroFlag = false;
				}
			}

			//RPC sync data
			networkView.RPC("setPuzzle2RPC",RPCMode.AllBuffered,randomAnswer[0],randomAnswer[1],randomAnswer[2]);

			//puzzle 3

			//clear  random answer
			randomAnswer.Clear();

			//create random answer
			for(int i = 0;i<puzzle3AnswerSize;i++)
			{
				//range is [0,1,2]
				int randomNum = Random.Range(0,3);
				randomAnswer.Add(randomNum);
			}

			//RPC sync data
			networkView.RPC("setPuzzle3RPC",RPCMode.AllBuffered,randomAnswer[0]);

		}

	}

	void Update()
	{
		//update
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
		//clear
		puzzle1Answer.Clear();

		puzzle1Answer.Add(i1);
		puzzle1Answer.Add(i2);
		puzzle1Answer.Add(i3);
	}

	[RPC]
	void setPuzzle2RPC(int i1,int i2,int i3)
	{
		//clear
		puzzle2Answer.Clear();
		
		puzzle2Answer.Add(i1);
		puzzle2Answer.Add(i2);
		puzzle2Answer.Add(i3);
	}

	[RPC]
	void setPuzzle3RPC(int i1)
	{
		//clear
		puzzle3Answer.Clear();
		
		puzzle3Answer.Add(puzzle3AnswerStringSet[i1]);
	}
}
