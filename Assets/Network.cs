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
		socket.On ("disconnected", OnDisconnected);
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

		var position = new Vector3 (GetFloatFromJson (e.data, "x"), 0, GetFloatFromJson (e.data, "y"));
		var player = players[e.data["id"].ToString()];

		var navigatePos = player.GetComponent<NavigatePosition>();
		
		navigatePos.NavigateTo (position);
	}

	void OnRegistered(SocketIOEvent e) {
	   Debug.Log("registered id: " + e.data);			
	}

	void OnDisconnected(SocketIOEvent e) {
		Debug.Log ("Disconnected: " + e.data);
		var id = e.data ["id"].ToString ();
		var player = players [id];
		Destroy (player);
		players.Remove (id);
    }

	float GetFloatFromJson(JSONObject data, string key) {
		return float.Parse (data [key].ToString ().Replace("\"", ""));
	}
}