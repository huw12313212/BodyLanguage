using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeGrowing : MonoBehaviour {
	public List<GameObject> particleSystemList;
	public TweenScale tween;

	public bool growed = false;
	public GameEndManagerScript gameEneManager;

	public bool grow
	{
		set
		{
			//for test
			Debug.Log("WorldTree's Grow set to "+value);

			tween.enabled  = value;
			growed = value;

			for(int i = 0;i<particleSystemList.Count;i++)
			{
				particleSystemList[i].SetActive(true);
			}

			//Stage clear,delete file
			if(value == true)
			{
				gameEneManager.gameEnd();
			}

		}

		get
		{
			return growed;
		}
	}
	
}
