using UnityEngine;
using System.Collections;

public class PuzzleDoorScript : MonoBehaviour {

	public TweenPosition tween;
	public Vector3 positionOffset;
	public MusicHandler musicManager;
	public float doorClosedBoundPositionY;
	//ths value is between 'door initial position y' and 'open offset y'

	public void open()
	{
		//check already open or not
		if(gameObject.transform.localPosition.y>=doorClosedBoundPositionY){
			//open door
			Debug.Log("Open the door");
			musicManager.playDoorOpenAudio();
			tween.from = gameObject.transform.localPosition;
			tween.to = gameObject.transform.localPosition + positionOffset;
			tween.Reset();
			tween.enabled = true;
		}
	}
}
