using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	public float gameDurationTime;
	// Use this for initialization
	void Start () {
		gameDurationTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameDurationTime += Time.deltaTime;
	}

	public float getDurationTime(){
		return gameDurationTime;
	}

	public void setTime(float time)
	{
		gameDurationTime = time;
	}
}
