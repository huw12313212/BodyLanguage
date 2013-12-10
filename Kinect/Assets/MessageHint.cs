using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageHint : MonoBehaviour {
	
	public List<int> message = new List<int>();
	public List<UILabel> Labels = new List<UILabel>();
	
	// Use this for initialization
	void Start () {
	
		Labels[0].text = message[0] + "";
				Labels[1].text = message[1] + "";
				Labels[2].text = message[2] + "";
				Labels[3].text = message[3] + "";
		
		Labels[2].text = "Press A";
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
