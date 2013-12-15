using UnityEngine;
using System.Collections;

public class PuzzleDoorScript : MonoBehaviour {

	public TweenPosition tween;

	public void open()
	{
		//open door
		Debug.Log("Open the door");
		tween.from = gameObject.transform.localPosition;
		tween.to = gameObject.transform.localPosition + new Vector3(0,-10.0f,0);
		tween.Reset();
		tween.enabled = true;
	}
}
