using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class networkData
{
	public float lastSynchronizationTime = Time.time;
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
	
	public List<GameObject> syncList;
	public List<networkData> dataList = new List<networkData>();

	public float HugTime = 0;
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

		string name = gameObject.name;

		//Debug.Log(name+"left"+syncList[14].transform.position+" right"+syncList[21].transform.position);

		if(syncList[14].transform.position.y>1.0&&
		   syncList[14].transform.position.y<=1.6&&
		   syncList[21].transform.position.y>1.0&&
		   syncList[21].transform.position.y<=1.6)
		{
			HugTime += Time.deltaTime;
		}
		else
		{
			HugTime = 0;
		}

	}
}
