using UnityEngine;
using System.Collections;

public class PuzzleDoorScript : MonoBehaviour {

	public TweenPosition tween;

	public void open()
	{
		Debug.Log("Open the door");
		tween.from = gameObject.transform.localPosition;
		tween.to = gameObject.transform.localPosition + new Vector3(0,-10.0f,0);
		tween.Reset();
		tween.enabled = true;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;

		if (stream.isWriting)
		{
			syncPosition = gameObject.transform.localPosition;
            stream.Serialize(ref syncPosition);			
		}
		else
		{

            stream.Serialize(ref syncPosition);
			gameObject.transform.localPosition = syncPosition;
		}
	}
}
