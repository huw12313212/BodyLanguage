using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeGrowing : MonoBehaviour {
	public List<GameObject> particleSystemList;
	public TweenScale tween;

	public bool grow
	{
		set
		{
			tween.enabled  = value;

			for(int i = 0;i<particleSystemList.Count;i++)
			{
				particleSystemList[i].SetActive(true);
			}
		}
	}


}
