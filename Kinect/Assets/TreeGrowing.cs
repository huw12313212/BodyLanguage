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
				//ParticleSystem particleSystem = particleSystemList[i].GetComponent<ParticleSystem>();
				//if(particleSystem != null) particleSystem.Play();
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		grow = true;
	}
}
