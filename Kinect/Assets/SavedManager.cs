using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class SavedManager : MonoBehaviour {
	public List<GameObject> savedObjectArray;
	private bool playerInit = false;
	private JSONObject allData;
	//private GameObject player;
	private bool clearFile = false;
	public GameTimer timer;

	public void Save()
	{
		Debug.Log("[SYSTEM] Saved.");
		//don't save
		if(clearFile == true) return;

		JSONObject SavedTable = new JSONObject();

		JSONObject savedObjectArrayJson = new JSONObject();

		foreach(GameObject gameObject in savedObjectArray)
		{
			JSONObject objectJson = new JSONObject();

			Transform transform = gameObject.transform;
			//position
			objectJson.AddField("x",transform.position.x);
			objectJson.AddField("y",transform.position.y);
			objectJson.AddField("z",transform.position.z);

			//rotation
			objectJson.AddField("rx",transform.rotation.x);
			objectJson.AddField("ry",transform.rotation.y);
			objectJson.AddField("rz",transform.rotation.z);

			//scale
			objectJson.AddField("sx",transform.localScale.x);
			objectJson.AddField("sy",transform.localScale.y);
			objectJson.AddField("sz",transform.localScale.z);

			//enable
			objectJson.AddField("active",gameObject.active);

			savedObjectArrayJson.Add(objectJson);
		}

		//add to table
		SavedTable.AddField("ObjectsData",savedObjectArrayJson);
		

		//Global Sync Object
		GlobalSyncData globalSyncData = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();

		if(globalSyncData!=null){
			JSONObject objectJsonTemp = new JSONObject();
			int i=0;
			//check
			if(globalSyncData.puzzle1Answer.Count == globalSyncData.puzzle1AnswerSize){
				//answer 1
				for(i = 0;i<globalSyncData.puzzle1AnswerSize;i++)
				{
					objectJsonTemp.AddField("answer1-"+i.ToString(),globalSyncData.puzzle1Answer[i]);
				}
			}

			if(globalSyncData.puzzle2Answer.Count == globalSyncData.puzzle2AnswerSize){
				//answer 2
				for(i = 0;i<globalSyncData.puzzle2AnswerSize;i++)
				{
					objectJsonTemp.AddField("answer2-"+i.ToString(),globalSyncData.puzzle2Answer[i]);
				}
			}

			if(globalSyncData.puzzle3Answer.Count == globalSyncData.puzzle3AnswerSize){
				//answer 3
				for(i = 0;i<globalSyncData.puzzle3AnswerSize;i++)
				{
					objectJsonTemp.AddField("answer3-"+i.ToString(),globalSyncData.puzzle3Answer[i]);
				}
			}

			//savedObjectArrayJson.Add(objectJsonTemp);
			SavedTable.AddField("AnswersData",objectJsonTemp);
		}

		//add player data
		GameObject player = CameraManager.CurrentPlayer1;
		if(player!=null)
		{
			JSONObject playerJsonTemp = new JSONObject();

			playerJsonTemp.AddField("x",player.transform.position.x);
			playerJsonTemp.AddField("y",player.transform.position.y);
			playerJsonTemp.AddField("z",player.transform.position.z);

			//onTop
			BotControlScript script = player.GetComponent<BotControlScript>();
			playerJsonTemp.AddField("onTop",script.onTop);
			
			//add to saved Table
			SavedTable.AddField("PlayerData",playerJsonTemp);
		}

		//save game during time
		if(timer != null)
		{
			JSONObject timeJsonTemp = new JSONObject();
			timeJsonTemp.AddField("durationTime",timer.getDurationTime());

			//add to saved Table
			SavedTable.AddField("TimeData",timeJsonTemp);
		}

		//Debug.Log(savedObjectArrayJson.ToString());

		//write to file
		StreamWriter _streamWriter = new StreamWriter("Assets/Resources/Save.txt",false);

		_streamWriter.Write(SavedTable.ToString());
		_streamWriter.Close();

	}

	public void WritePuzzle3Count(int count)
	{
		Debug.Log("Puzzle 3 Saved.");

		//add puzzle count
		//get Global data object
		GameObject globalObject = GameObject.FindGameObjectWithTag("GlobalSyncObject");
		GlobalSyncData globalSyncObject = globalObject.GetComponent<GlobalSyncData>();

		if(globalSyncObject!=null) count = (count+1)%(globalSyncObject.puzzle3AnswerStringSet.Count);
		else count = (count+1)%3;

		JSONObject SavedTable = new JSONObject();

		SavedTable.AddField("puzzle3Count",count);

		//write to file
		StreamWriter _streamWriter = new StreamWriter("Assets/Resources/SavePuzzle3.txt",false);
		
		_streamWriter.Write(SavedTable.ToString());
		_streamWriter.Close();
		
	}

	public int LoadPuzzle3Count()
	{
		//check file exist
		if(!File.Exists("Assets/Resources/SavePuzzle3.txt")) return 0;

		//read file
		StreamReader _streamReader = new StreamReader("Assets/Resources/SavePuzzle3.txt");
		
		String allStrings =_streamReader.ReadToEnd();
		
		allData = new JSONObject(allStrings);

		return (int)allData.GetField("puzzle3Count").n;
	}


	public void Load()
	{

		if(!Directory.Exists("Assets"))
		{
			Directory.CreateDirectory("Assets");
		}

		if(!Directory.Exists("Assets/Resources/"))
		{
			Directory.CreateDirectory("Assets/Resources/");
		}

		//check file exist
		if(!File.Exists("Assets/Resources/Save.txt")) return;
		//read file
		StreamReader _streamReader = new StreamReader("Assets/Resources/Save.txt");
	
		String allStrings =_streamReader.ReadToEnd();

		allData = new JSONObject(allStrings);

		JSONObject arrayDataJsonObject = allData.GetField("ObjectsData");
		JSONObject timeDataJsonObject = allData.GetField("TimeData");
		//JSONObject answerDataJsonObject = allData.GetField("AnswersData");
		//JSONObject playerDataJsonObject = allData.GetField("PlayerData");

		//Debug.Log ("ObjectsData:"+arrayDataJsonObject.ToString());
		//Debug.Log ("AnswersData:"+answerDataJsonObject.ToString());

		//set loaded data to objects
		int count = 0;
		foreach(GameObject gameObject in savedObjectArray)
		{
			Transform transform = gameObject.transform;
			JSONObject objectJSONObject = arrayDataJsonObject[count];
			//position

			Vector3 position;

			position.x = (float)objectJSONObject.GetField("x").n;
			position.y = (float)objectJSONObject.GetField("y").n;
			position.z = (float)objectJSONObject.GetField("z").n;
			transform.position = position;
			
			//rotation
			Vector3 rotation;

			rotation.x = (float)objectJSONObject.GetField("rx").n;
			rotation.y = (float)objectJSONObject.GetField("ry").n;
			rotation.z = (float)objectJSONObject.GetField("rz").n;

			transform.rotation = Quaternion.Euler(rotation);
			//scale
			Vector3 scale;
			scale.x = (float)objectJSONObject.GetField("sx").n;
			scale.y = (float)objectJSONObject.GetField("sy").n;
			scale.z = (float)objectJSONObject.GetField("sz").n;

			transform.localScale = scale;

			//enable
			gameObject.active = objectJSONObject.GetField("active").b;

			//gameObject.transform = transform;
			count++;
		}

		//load time data
		if(timer!=null){
			timer.setTime((float)timeDataJsonObject.GetField("durationTime").n);
		}
	}

	public void Clear()
	{
		Debug.Log("[SYSTEM]Clear saved!");


		//delete file
		if(File.Exists("Assets/Resources/Save.txt")){
			File.Delete("Assets/Resources/Save.txt");
		}

		//set flag
		clearFile = true;

		//add puzzle count 
		//Global Sync Object
		GlobalSyncData globalSyncData = GameObject.FindGameObjectWithTag("GlobalSyncObject").GetComponent<GlobalSyncData>();
		if(globalSyncData!=null)WritePuzzle3Count(globalSyncData.getPuzzle3Index(0));
	}


	// Use this for initialization
	void Start () {
		Load();

	}

	void initPlayerData()
	{
		if(allData!=null)
		{
			JSONObject playerDataJsonObject = allData.GetField("PlayerData");

			//no player data
			if(playerDataJsonObject == null)
			{
				playerInit = true;
				return;
			}

			GameObject player = CameraManager.CurrentPlayer1;


			if(player!=null)
			{
				//get player transform
				Transform transform = player.transform;
				//set position

				Vector3 position = new Vector3();
				
				position.x = (float)playerDataJsonObject.GetField("x").n;
				position.y = (float)playerDataJsonObject.GetField("y").n;
				position.z = (float)playerDataJsonObject.GetField("z").n;
				transform.position = position;

				//onTop
				BotControlScript script = player.GetComponent<BotControlScript>();
				script.onTop = playerDataJsonObject.GetField("onTop").b;

				//Debug.Log ("Robot pos:"+CameraManager.CurrentPlayer1.transform.position.ToString());

				playerInit = true;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(playerInit!=true)
		{
			initPlayerData();
		}

		//delete saved file
		if(Input.GetKeyDown(KeyCode.F10))
		{
			Clear ();
			//clearFile = true;
		}

	}
}
