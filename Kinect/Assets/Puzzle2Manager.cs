using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle2Manager : MonoBehaviour {
	
	private List<int> CodeList;
	public List<GameObject> controlObjectList;
	public List<GameObject> controlPanelSensorList;
	public List<int> inputCode;
	private bool puzzleSolve;

	[RPC]
	public void Code(int index,int value)
	{
		//get Global data object
		GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
		GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();
		
		//check
		if(globalSyncObject == null) return;
		//get answer
		CodeList = globalSyncObject.puzzle2Answer;
		//check
		if((CodeList == null) || (CodeList.Count == 0)) return;
		
		//add input code to list
		inputCode[index] = value;

		//update rotation
		updatePanelRotation(index);

		//check input code size
		if(inputCode.Count == globalSyncObject.puzzle2AnswerSize)
		{
			//solve flag
			puzzleSolve = true;
			
			//check code
			for(int count = 0;count <CodeList.Count;count++)
			{
				if(inputCode[count] != CodeList[count]) puzzleSolve = false;
			}
			
			//check compare result
			if(puzzleSolve == true)
			{
				//open control object
				foreach (GameObject gameobj in controlObjectList)
				{
					//get control object script
					PuzzleDoorScript script = gameobj.GetComponent<PuzzleDoorScript>();
					if(script!=null) script.open();
				}
			}
			
			//clear input code
			//inputCode.RemoveAt(0);
		}
	}

	// Update is called once per frame
	void Update()
	{
//		Debug.Log ("Input :"+inputCode[0]+" "+inputCode[1]+" "+inputCode[1]);
	}

	// Use this for initialization
	void Start () {
		//init
		inputCode = new List<int>();
		inputCode.Add(0);
		inputCode.Add(0);
		inputCode.Add(0);
		puzzleSolve = false;
	}

	void updatePanelRotation(int index){
		//get sensor
		Puzzle2PanelSensor sensor = controlPanelSensorList[index].GetComponent<Puzzle2PanelSensor>();
		//update rotation
		sensor.UpdatePanelRotation();
	}
	
}
