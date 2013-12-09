﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class networkData
{
	public float lastSynchronizationTime = 0f;
	public float syncDelay = 0f;
	public float syncTime = 0f;
	public Vector3 syncStartPosition = Vector3.zero;
	public Vector3 syncEndPosition = Vector3.zero;
	public Quaternion syncStartRotation = Quaternion.Euler(Vector3.zero);
	public Quaternion syncEndRotation = Quaternion.Euler(Vector3.zero);
	public Vector3 syncPosition = Vector3.zero;
	public Vector3 syncVelocity = Vector3.zero;
	public Quaternion syncRotation = Quaternion.Euler(Vector3.zero);

}

public class NodeManager : MonoBehaviour {
	
	public List<GameObject> syncList = new List<GameObject>();
	public List<networkData> dataList = new List<networkData>();
	//public int i = 0;
	// Use this for initialization
	void Start () {
		
		foreach(GameObject gameObject in syncList)
		{
			dataList.Add(new networkData());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
