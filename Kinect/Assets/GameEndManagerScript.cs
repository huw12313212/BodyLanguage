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
		//stop timer
		timer.stop();

		//delete file
		SavedManager savedManager = GameObject.FindGameObjectWithTag("SavedManager").GetComponent<SavedManager>();
		if(savedManager!=null) {
			savedManager.Clear();
		}

		//send game duration time to server
		if(Network.isServer)
		{
			//if(timer!=null) sendHttpRequest("sendGameDurationTime?time="+timer.getDurationTime(),requsetMode.insertGameTime);
			if(timer!=null) sendHttpRequest("addRecord",requsetMode.insertGameTime);
		}

	}

	public void sendHttpRequest(string param,requsetMode mode)
	{	
		//get mongodb server
		if(NetworkManager.mongoDBServer != "") serverURL = NetworkManager.mongoDBServer+"/";
		string url = serverURL+param;

		Debug.Log ("server url:"+url);
		//WWW www = new WWW(url);
		if(mode == requsetMode.getLeaderList)
		{
			//GET
			WWW www = new WWW(url);
			StartCoroutine(WaitForGetLeaderListRequest(www));
		}
		else if (mode == requsetMode.insertGameTime) 
		{
			//POST
			var form = new WWWForm();
			form.AddField("username1", "you");
			form.AddField("username2", "you");
			form.AddField("exeTime", timer.getDurationTime().ToString());

			WWW www = new WWW(url,form.data,form.headers);

			StartCoroutine(WaitForInsertTimeRequest(www));
		}
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
			//Get response JSON
			JSONObject responseJSONObject = new JSONObject(www.data.Replace(" ","").Replace("_"," "));
			Debug.Log("Response WWW: " + www.data);
			Debug.Log("Response JSON: " + responseJSONObject);

			//set leader board data and show
			leaderboard.setData(responseJSONObject);

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}   
	}

	void Update () {

		//Fast Game End
		if(Input.GetKeyDown(KeyCode.F12))
		{
			//tree grow
			GlobalSyncData sync = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
			sync.syncWorldTree();

			//game end
			//gameEnd ();
		}
		
	}

}
