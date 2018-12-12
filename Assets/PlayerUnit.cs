using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// A  Player Unit is a unit controlled by a player
//this could be a character in a FPS, a zergling in RTS
//Or a scout in a TBS
public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//This function runs on all player units
		//How i verified that im allowd to mess around with this object
		
		if ( hasAuthority == false){
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			this.transform.Translate(0,1,0);
		}

		
		if (Input.GetKeyDown(KeyCode.D)) {
			Destroy(gameObject);
		}
	}
}
