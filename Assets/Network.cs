using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {
	public GameObject playerPrefab;
	static SocketIOComponent socket;

	Dictionary<string,GameObject> players;

	// Use this for initialization
	void Start () {
		socket = GetComponent<SocketIOComponent> ();
		socket.On ("open", OnConnected);
		socket.On ("spawn", OnSpawned);
		socket.On ("move", OnMove);
		socket.On ("registered", OnRegistered);
		players = new Dictionary<string,GameObject> ();
	}

	
	void OnConnected(SocketIOEvent e){
		Debug.Log ("connected");
	}

	void OnSpawned (SocketIOEvent e) {
		Debug.Log ("Spawned" + e.data);
		var player = Instantiate (playerPrefab);

		players.Add (e.data ["id"].ToString (), player);
		Debug.Log ("count: " + players.Count);
	}

	void OnMove(SocketIOEvent e) {
		Debug.Log ("player is moving " + e.data);
	}

	void OnRegistered(SocketIOEvent e) {
	   Debug.Log("registered id: " + e.data);			
	}
}
