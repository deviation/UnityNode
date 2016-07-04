using UnityEngine;
using System.Collections;
using SocketIO;

public class NetworkMove : MonoBehaviour {

	public SocketIOComponent socket;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMove(Vector3 position)
	{
		// Send pos to nodevar nav
		Debug.Log("sending position to node" + VectorToJson(position));
		socket.Emit ("move", new JSONObject(VectorToJson(position)));
	}

	string VectorToJson(Vector3 vector)
	{
		return string.Format (@"{{""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.z);
	}
}
