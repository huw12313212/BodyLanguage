using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour {
	
	private List<int> CodeList;
	public List<GameObject> controlObjectList;
	private List<int> inputCode;
	private bool puzzleSolve;

	public void Code(int i)
	{
		Debug.Log ("Code :"+i);

		//get Global data object
		GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
		GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

		//get answer
		CodeList = globalSyncObject.puzzle1Answer;
		
		//add input code to list
		inputCode.Add(i);

		//check
		if(globalSyncObject == null) return;

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
			inputCode.Clear();
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
