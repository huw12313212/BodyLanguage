using UnityEngine;
using System.Collections;

public class GameEndManagerScript : MonoBehaviour {
	public GameTimer timer;
	public string serverURL;
	public LeaderBoardManager leaderboard;
	public string test;
	public enum requsetMode
	{
		insertGameTime,
		getLeaderList
	}

	public void gameEnd()
	{
		//delete file
		SavedManager savedManager = GameObject.FindGameObjectWithTag("SavedManager").GetComponent<SavedManager>();
		if(savedManager!=null) {
			savedManager.Clear();
		}

		//send game duration time to server
		//CameraManager.CurrentPlayer1.name = "RobotPlayer(Clone)";
		if(Network.isServer)
		{
			//if(timer!=null) sendHttpRequest("sendGameDurationTime?time="+timer.getDurationTime(),requsetMode.insertGameTime);
			if(timer!=null) sendHttpRequest("orderlist",requsetMode.insertGameTime);
		}

		//send game duration time
		//if(timer!=null) sendHttpRequest("gameDurationTime?="+timer.getDurationTime().ToString());
	}

	public void sendHttpRequest(string param,requsetMode mode)
	{
		string url = serverURL+param;
		WWW www = new WWW(url);
		if(mode == requsetMode.getLeaderList) StartCoroutine(WaitForGetLeaderListRequest(www));
		else if (mode == requsetMode.insertGameTime) StartCoroutine(WaitForInsertTimeRequest(www));
	}

	IEnumerator WaitForInsertTimeRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			//use RPC to call send http request
			//Global Sync Object
			GlobalSyncData globalSyncData = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
			globalSyncData.getAndShowLeaderListRPC();

			//if(timer!=null) sendHttpRequest("orderlist",requsetMode.getLeaderList);

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}   
	}

	IEnumerator WaitForGetLeaderListRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			//JSONObject responseJSONObject = new JSONObject(www.data.Replace("\n","").Replace("\r","").Replace("\r\n",""));
			JSONObject responseJSONObject = new JSONObject(www.data.Replace(" ","").Replace("_"," "));
			Debug.Log("Response WWW: " + www.data);
			Debug.Log("Response JSON: " + responseJSONObject);

			//set leader board data and show
			leaderboard.setData(responseJSONObject);

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}   
	}

}
