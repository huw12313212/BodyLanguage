using UnityEngine;
using System.Collections;

public class PuzzleDoorScript : MonoBehaviour {

	public TweenPosition tween;
	public Vector3 positionOffset;

	public void open()
	{
		//open door
		Debug.Log("Open the door");
		tween.from = gameObject.transform.localPosition;
		tween.to = gameObject.transform.localPosition + positionOffset;
		tween.Reset();
		tween.enabled = true;
	}
}
