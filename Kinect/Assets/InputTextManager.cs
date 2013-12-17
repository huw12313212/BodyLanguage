using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputTextManager : MonoBehaviour {
	private int currentInputCount;
	public List <GameObject> textList;
	public List<GameObject> controlObjectList;
	public int currentAnswerIndex;

	// Use this for initialization
	void Start () {
		currentInputCount = 0;
		currentAnswerIndex = 0;
	}

	[RPC]
	public void Code(string str)
	{
		//set text
		if(currentInputCount<textList.Count)
		{
			TextMesh textMesh = textList[currentInputCount].GetComponent<TextMesh>();
			textMesh.text = str;
			currentInputCount++;
		}
		else
		{
			//clear text
			foreach(GameObject textObject in textList)
			{
				TextMesh textMesh = textObject.GetComponent<TextMesh>();
				textMesh.text = "";
			}
			//reset count
			currentInputCount = 0;
		}

		//check answer
		if(checkAnswer() == true)
		{
			//open control object
			foreach (GameObject gameobj in controlObjectList)
			{
				//get control object script
				PuzzleDoorScript script = gameobj.GetComponent<PuzzleDoorScript>();
				if(script!=null) script.open();
			}
		}

	}

	private bool checkAnswer()
	{
		bool success = false;

		//get Global data object
		GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
		GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();
		
		//check
		if(globalSyncObject == null) return false;

		//get answer
		List<string> puzzle3Answer = globalSyncObject.puzzle3Answer;
	
		//check
		if((puzzle3Answer == null) || (puzzle3Answer.Count == 0)) return false;

		//input string
		string inputStr = "";

		//set input code
		for(int count = 0;count <textList.Count;count++)
		{
			TextMesh textMesh = textList[count].GetComponent<TextMesh>();
			inputStr += textMesh.text;
		}

		//check input string 
		if(inputStr == puzzle3Answer[currentAnswerIndex])
		{
			if(currentAnswerIndex<(globalSyncObject.puzzle3AnswerSize-1)) currentAnswerIndex ++;
			return true;
		}
		else return false;

	}


}
