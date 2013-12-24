using UnityEngine;
using System.Collections;

public class PuzzleDoorScript : MonoBehaviour {

	public TweenPosition tween;
	public Vector3 positionOffset;
	public MusicHandler musicManager;

	public void open()
	{
		//open door
		Debug.Log("Open the door");
		musicManager.playDoorOpenAudio();
		tween.from = gameObject.transform.localPosition;
		tween.to = gameObject.transform.localPosition + positionOffset;
		tween.Reset();
		tween.enabled = true;
	}
}
