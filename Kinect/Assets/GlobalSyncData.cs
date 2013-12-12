using UnityEngine;
using System.Collections;

public class GlobalSyncData : MonoBehaviour {

	public int test;

	// Use this for initialization
	void Start () {
		Debug.Log("Start!");
		test = 0;
	}

	void Update()
	{
		//test++;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		int syncTest = 0;
		
		if (stream.isWriting)
		{
			Debug.Log("Wirting!");
			syncTest = test;
			stream.Serialize(ref syncTest);			
		}
		else
		{
			Debug.Log("Received!");
			stream.Serialize(ref syncTest);
			test = syncTest;
		}
	}
}
