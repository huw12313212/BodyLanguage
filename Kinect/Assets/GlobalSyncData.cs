using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSyncData : MonoBehaviour {

	//answer size
	public int puzzle1AnswerSize;
	public int puzzle2AnswerSize;
	public int puzzle3AnswerSize;
	//answer
	public List<int> puzzle1Answer;
	public List<int> puzzle2Answer;
	public List<string> puzzle3Answer;
	public List<string> puzzle3AnswerStringSet;
	
	//puzzle manager
	private GameObject puzzle1Manager;
	private GameObject puzzle2Manager;
	private GameObject puzzle3Manager;
	
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

		//get puzzle manager
		puzzle1Manager = GameObject.Find ("Puzzle 1 Manager");
		puzzle2Manager = GameObject.Find ("Puzzle 2 Manager");
		puzzle3Manager = GameObject.Find ("Puzzle 3 Manager");

		//init puzzle answer
		//if(networkView.isMine)
		//{
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
			
		//}
		
	}

	//sync all client tree
	public void syncWorldTree()
	{
		networkView.RPC("setTreeGrowRPC",RPCMode.AllBuffered);
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
			//Debug.Log("Wirting!");
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
			//Debug.Log("Received!");
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

	//Tree grow
	[RPC]
	void setTreeGrowRPC()
	{
		Debug.Log("Sync Tree Growing!");
		//sync all grow variable
		GameObject worldTree = GameObject.FindGameObjectWithTag("WorldTree");
		TreeGrowing treeGrowingScript = worldTree.GetComponent<TreeGrowing>();
		treeGrowingScript.grow = true;
	}

	[RPC]
	void getAndShowLeaderList()
	{
		//Global Sync Object
		GameEndManagerScript gameEndManager = GameObject.FindGameObjectWithTag("GameEndManager").GetComponent<GameEndManagerScript>();
		gameEndManager.sendHttpRequest("orderlist",GameEndManagerScript.requsetMode.getLeaderList);
	}

	public void getAndShowLeaderListRPC()
	{
		networkView.RPC("getAndShowLeaderList",RPCMode.AllBuffered);
	}

	//puzzle 1 
	public void triggerPuzzle1Input(int data)
	{
		NetworkView networkView =  puzzle1Manager.GetComponent<NetworkView>();
		if(networkView != null) networkView.RPC("Code",RPCMode.AllBuffered,data);
	}

	//puzzle 2
	public void triggerPuzzle2Input(int index,int value)
	{
		NetworkView networkView =  puzzle2Manager.GetComponent<NetworkView>();
		if(networkView != null) networkView.RPC("Code",RPCMode.AllBuffered,index,value);
	}

	//puzzle 3 
	public void triggerPuzzle3Input(string data)
	{
		NetworkView networkView =  puzzle3Manager.GetComponent<NetworkView>();
		if(networkView != null) networkView.RPC("Code",RPCMode.AllBuffered,data);
	}
}
