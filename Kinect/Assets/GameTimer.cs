using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	public float gameDurationTime;
	private bool stopCount = false;
	// Use this for initialization
	void Start () {
		gameDurationTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopCount == false) gameDurationTime += Time.deltaTime;
	}

	public float getDurationTime(){
		return gameDurationTime;
	}

	public void setTime(float time)
	{
		gameDurationTime = time;
	}

	public void stop()
	{
		stopCount = true;
	}
}
