using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class NetworkManager : MonoBehaviour
{
	public string typeName = "BodyLanguageBL";
	private const string gameName = "RoomName24";
	
	private bool isRefreshingHostList = false;
	private HostData[] hostList;
	private float startTime;
	public GameObject playerPrefab;
	public GameObject playerPrefab2;
	public GameObject globalSyncObject;
	bool flagServer = false;
	bool flagClient = false;
	public GameTimer timer;

	public CameraManager cameraManager;
	public Vector3 serverPlayerInitialPosition = Vector3.zero;
	public Quaternion serverPlayerInitialRotation = Quaternion.identity;
	static private int serverPort = 5566;

	/*void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }*/
	void Start(){
		startTime = Time.time;
	}
	private void StartServer()
	{
		Network.InitializeServer(5, serverPort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer();
		//create sync object
		GameObject syncObject = (GameObject)Network.Instantiate(globalSyncObject, Vector3.zero, Quaternion.identity, 0);
		Debug.Log ("Initialized");
	}
	
	
	void Update()
	{

		//check the room name of the game
		/*if (hostList.Length > 0) {
			string name = "";
			for(int i = 0;i < hostList.Length;i++){
				name += hostList[i].gameName;
				name += " ";
			}
			Debug.Log("NAME: " + name);
		}*/
		
		//check wether the server is built or not 
		if (flagServer == false && flagClient == false) {

			//if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
			//{
			RefreshHostList();
			hostList = MasterServer.PollHostList();
			
			//check server config exist or not ,if exist ,connect directly
			flagClient = LoadServerConfig();

			if (Time.time - startTime > 1.5f) {
				Debug.Log("update: "+hostList.Length);
				//Debug.Log("passed time gap!?qqqqq");
				if (hostList.Length == 0) {
					StartServer ();
					//JoinServer(null);
					flagServer = true;
				} 
				else {
					Debug.Log ("I am the client.");
					for (int i = 0; i < hostList.Length; i++) {
						if (hostList [i].gameName == gameName) {
							JoinServer (hostList[i]);
							flagClient = true;
							break;
						}
					}
				}
			}
		}
		//isRefreshingHostList = false;

	}
	
	private void RefreshHostList()
	{
		//Debug.Log ("RefreshHostList");
		if (!isRefreshingHostList)
		{
			//isRefreshingHostList = true;
			//MasterServer.RequestHostList(typeName);
			
			MasterServer.RequestHostList(typeName);
			hostList = MasterServer.PollHostList();
			//Debug.Log("hostList: "+hostList);
			
		}
	}
	
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		Debug.Log ("I connect to the server: " + hostData.gameName);
		
	}
	
	void OnConnectedToServer()
	{
		Debug.Log ("connected to server QQ");
		SpawnPlayer2();

	}
	
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject)Network.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0);
	}

	private void SpawnPlayer2()
	{
		GameObject player = (GameObject)Network.Instantiate(playerPrefab2, Vector3.zero, Quaternion.identity, 0);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		//saved state to file
		SavedManager savedManager = GameObject.FindGameObjectWithTag("SavedManager").GetComponent<SavedManager>();
		savedManager.Save();

		if (Network.isServer)
		{
			//server offline, server will calls
			Debug.Log("Local server connection disconnected");
		}
		else if (info == NetworkDisconnection.LostConnection)
		{
			Debug.Log("Lost connection to the servser");
		}
		else
		{
			//Server offline , client will call
			//Client offline ,client will call
			Debug.Log("Successfully diconnected from the server");

			//destory player
			Network.Destroy(cameraManager.Player2.GetComponent<NetworkView>().viewID);

			//stop timer
			timer.stop();
		}

	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		//client offline ,server will call
		Debug.Log("Player connection disconnected "+player.ipAddress);	

		Network.Destroy(cameraManager.Player2.GetComponent<NetworkView>().viewID);

		//stop timer
		timer.stop();
	} 

	void OnPlayerConnected(NetworkPlayer player){

		//send self position
		Vector3 currentPlayerPosition = CameraManager.CurrentPlayer1.transform.position;
		Quaternion currentPlayerRotation = CameraManager.CurrentPlayer1.transform.rotation;
		if(currentPlayerPosition!=null){
			Debug.Log ("Send Server Player Data!");
			networkView.RPC("sendServerPlayerData",RPCMode.Others,currentPlayerPosition,currentPlayerRotation);
		}
	}

	bool LoadServerConfig(){

		if(!File.Exists("Assets/Resources/ServerConfig.txt")) return false;
		//read file
		StreamReader _streamReader = new StreamReader("Assets/Resources/ServerConfig.txt");
		
		String allStrings =_streamReader.ReadToEnd();
		
		JSONObject allData = new JSONObject(allStrings);
		
		string serverIP = allData.GetField("serverIP").str;
		int serverPort = (int)allData.GetField("serverPort").n;
		Debug.Log("ServerIP:"+serverIP+" port:"+serverPort);

		//connect
		NetworkConnectionError error = Network.Connect(serverIP,serverPort);

		if(error == NetworkConnectionError.NoError) return true;
		else return false;
	}

	[RPC]
	void sendServerPlayerData(Vector3 playerPosition,Quaternion playerRotation){
		Debug.Log ("Receive Server Player Data!"+playerPosition);

		//player 1 list
		serverPlayerInitialPosition = playerPosition;
		serverPlayerInitialRotation = playerRotation;

	}

}
