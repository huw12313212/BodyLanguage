using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {
	
	public List<int> CodeList;
	public List<GameObject> controlObjectList;
	public List<GameObject> buttonObjectList;
	public List<int> inputCode;
	private bool puzzleSolve;

	[RPC]
	public void Code(int i)
	{
		Debug.Log ("Code :"+i);

		//get Global data object
		GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
		GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

		//let button i start animation
		if(buttonObjectList[i]!=null){
			buttonObjectList[i].GetComponent<ButtonClicked>().buttonClickAnim();
		}

		//check
		if(globalSyncObject == null) return;
		//get answer
		CodeList = globalSyncObject.puzzle1Answer;
		//check
		if((CodeList == null) || (CodeList.Count == 0)) return;

		//add input code to list
		inputCode.Add(i);

		//check input code size
		if(inputCode.Count == globalSyncObject.puzzle1AnswerSize)
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
			inputCode.RemoveAt(0);
		}


	}

	// Use this for initialization
	void Start () {
		//init
		inputCode = new List<int>();
		puzzleSolve = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
