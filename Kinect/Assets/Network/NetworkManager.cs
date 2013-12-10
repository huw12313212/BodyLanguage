using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private const string typeName = "BodyLanguageBL";
	private const string gameName = "RoomName24";
	
	private bool isRefreshingHostList = false;
	private HostData[] hostList;
	private float startTime;
	public GameObject playerPrefab;
	bool flagServer = false;
	bool flagClient = false;
	
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
		Network.InitializeServer(5, 25002, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer();
		Debug.Log ("Initialized");
	}
	
	
	void Update()
	{
		
		//if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
		//{
		RefreshHostList();
		hostList = MasterServer.PollHostList();
		
		
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
			if (Time.time - startTime > 3.0f) {
				Debug.Log("update: "+hostList.Length);
				Debug.Log("passed time gap!?qqqqq");
				if (hostList.Length == 0) {
					StartServer ();
					flagServer = true;
				} 
				else {
					Debug.Log ("I am the client,,,,,,,");
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
		SpawnPlayer();
		
	}
	
	
	private void SpawnPlayer()
	{
		GameObject player = (GameObject)Network.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0);
		
	}
}
