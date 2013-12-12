using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class createPuzzle1Answer : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		
		if(other.name.Contains("Rumbo"))
		{
			Debug.Log("Trigger Item");

			//get Global data object
			GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
			GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();
			
			if(globalSyncObject!=null)
			{
				Debug.Log("Create Random Num = ");
				List<int> randomAnswer = new List<int>();
				for(int i = 0;i<globalSyncObject.puzzle1AnswerSize;i++)
				{
					randomAnswer.Add(Random.Range(0,3));
				}
				globalSyncObject.setPuzzle1Answer(randomAnswer);
			}

			Destroy(gameObject);
		}
		
	}
}
