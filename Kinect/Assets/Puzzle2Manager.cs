using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle2Manager : MonoBehaviour {
	
	private List<int> CodeList;
	public List<GameObject> controlObjectList;
	private List<int> inputCode;
	private bool puzzleSolve;
	public int puzzle2AnswerSize;

	public void Code(int index,int value)
	{
		Debug.Log ("Index :"+index+" value:"+value);
		inputCode[index] = value;
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log ("Input :"+inputCode[0]+" "+inputCode[1]+" "+inputCode[1]);
	}

	// Use this for initialization
	void Start () {
		//init
		inputCode = new List<int>();
		inputCode.Add(-1);
		inputCode.Add(-1);
		inputCode.Add(-1);
		puzzleSolve = false;
		puzzle2AnswerSize = 3;
	}

}
