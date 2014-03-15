using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {
	public AudioClip doorOpenAudio;
	public AudioClip buttonClickAudio;
	// Use this for initialization
	void Start () {
	
	}
	

	public void playDoorOpenAudio()
	{
		if(doorOpenAudio!=null) audio.PlayOneShot(doorOpenAudio);
	}

	public void playButtonClickAudio()
	{
		if(buttonClickAudio!=null) audio.PlayOneShot(buttonClickAudio);
	}


}
