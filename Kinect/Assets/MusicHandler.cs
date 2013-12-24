using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {
	public AudioClip doorOpenAudio;

	// Use this for initialization
	void Start () {
	
	}
	

	public void playDoorOpenAudio()
	{
		audio.PlayOneShot(doorOpenAudio);
	}
}
