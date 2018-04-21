using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerTraits : NetworkBehaviour {

	public Color remotePlayerColor = Color.blue;
	
	public Color localPlayerColor;

	void Start() {
		localPlayerColor = GetComponent<MeshRenderer>().material.color;
	}

	public override void OnStartClient() {
		if (!isLocalPlayer) {
			// modified from Unity networking tutorial
			gameObject.GetComponent<MeshRenderer>().material.color = remotePlayerColor;
		}
	}

	public override void OnStartLocalPlayer() {
		gameObject.GetComponent<MeshRenderer>().material.color = localPlayerColor;
	}
}
