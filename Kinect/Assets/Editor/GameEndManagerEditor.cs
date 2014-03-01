using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameEndManagerScript))]
public class GameEndManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		GameEndManagerScript myScript = (GameEndManagerScript)target;
		if(GUILayout.Button("Test Game End!"))
		{
			myScript.gameEnd();
		}

		if(GUILayout.Button("JSONTest"))
		{
			//myScript.gameEnd();

			JSONObject json = new JSONObject(myScript.test);
			Debug.Log("test result:"+json);
		}
	}
}