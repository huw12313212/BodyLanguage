using UnityEngine;
using System.Collections;

public class Level3HintMessage : MonoBehaviour {

	//public 
	public UILabel label;

	void Awake()
	{
		GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
		//labl.

		label.text = sync.puzzle3Answer[0];

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
