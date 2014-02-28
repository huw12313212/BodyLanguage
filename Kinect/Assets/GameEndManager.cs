using UnityEngine;
using System.Collections;

public class GameEndManager : MonoBehaviour {
	public GameTimer timer;
	public string serverURL = "http://example.com/";
	public void gameEnd()
	{
		//delete file
		SavedManager savedManager = GameObject.FindGameObjectWithTag("SavedManager").GetComponent<SavedManager>();
		if(savedManager!=null) {
			savedManager.Clear();
		}

		//send game duration time
		if(timer!=null) sendHttpRequest("gameDurationTime?="+timer.getDurationTime().ToString());
	}

	private void sendHttpRequest(string param)
	{
		WWW www = new WWW(serverURL+param);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

}
