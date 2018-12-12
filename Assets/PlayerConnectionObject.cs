using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerConnectionObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		//check if is my pwn
		if (isLocalPlayer == false){
			//Belong to other player
			return;
		}

		//Since is invicible and not part of the world
		
		//Instantiate only creates an object on the local
		//even if it has NetworkOdentity still will not existe on the netwok
		//and therefor enot any other client
		//unless NetworkServer.Spawn() is called


		Debug.Log("PlayerObject start --- wuuu");
		//Instantiate(PlayerUnitPreface);
		// Command the server to SPAWN our unit
		CmdSpawnMyUnit();
	}

	public GameObject PlayerUnitPreface;

	//ARE VARIABLES WHERE  if their value changes on the server, then all clients arre
	//automacally inform of the new value

	[SyncVar(hook="OnPlayerNameChanged")]
	public string PlayerName = "Anonymous";

	// Update is called once per frame
	void Update () {
		//remenber Update runs on everyones computer, where or not they own thihs
		//particular player object
		if ( isLocalPlayer == false){
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CmdSpawnMyUnit();
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			string n = "Quill" + Random.Range(1, 1000);
			Debug.Log("===Sending the sevrer a request to change namee=== "+n);
			CmdChangePLayerName(n);
		}
	}

	void OnPlayerNameChanged(string newName){
		Debug.Log("OnPlayerNameChanged: Old name: "+PlayerName +" New name:" + newName);
		//WARNING: If you use hook on a syncVar then our
		//local value does not get automatically updated		
		PlayerName = newName;
		gameObject.name = "PlayerConnectionObject["+newName+"]";
	}

	//////////////////////////COMMANDS
	//are special function that only get executed on the serbver
	GameObject myPlayerUnit;


	[Command]
	void CmdSpawnMyUnit()
	{
		//We are guaranteed to be on the server right now
		GameObject go = Instantiate(PlayerUnitPreface);
		//Now that the object exist on the server, propagate it to all
		//the clients (and wire uo the netwo ident)
		myPlayerUnit = go;

		// go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient);

		NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
	}

	[Command]
	void CmdMoveUnitUp()
	{
		if (myPlayerUnit == null){
			return;
		}
		myPlayerUnit.transform.Translate(0,1,0);
	}

	[Command]
	void CmdChangePLayerName(string n){
		Debug.Log("CmdChangePLayerName: "+ n);
		//If there is a bad word, we just ignore teh request or send teh original name
		 
		PlayerName = n;

		//Maybe we should checks that the bame doesn have balcklisted words



		//Tell the other players what is this players's name now is
		// RpcChangePlayerName(n);
	}


	//////////////////////////COMMANDS
	//RPC are special functions that only get executed on the clients

	[ClientRpc]
	void RpcChangePlayerName(string n){
		Debug.Log("RpcChange player name: Change the player nam e on a particular PlayerConnectionObject"+n);
		PlayerName = n;
	}
}
