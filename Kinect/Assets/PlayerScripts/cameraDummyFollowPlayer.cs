using UnityEngine;
using System.Collections;

public class cameraDummyFollowPlayer : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
	
	}

	//set player
	public void setPlayer(GameObject playerObject)
	{
		player = playerObject;
	}

	// Update is called once per frame
	void Update () {
		if(player!=null) this.transform.position = player.transform.position;
		Debug.Log("i'm dummy!");
	}
}
