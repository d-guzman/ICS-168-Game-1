using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCameraSpawner : NetworkBehaviour {

	public GameObject cameraPrefab;
	public testCharacterDirection aimController;

	public GameObject cam;

	// void awake() {
	// 	if (!isLocalPlayer) {
	// 		return;
	// 	}

	// 	cam = Instantiate(cameraPrefab);
	// 	testNetworkCamera camera = cam.GetComponent<testNetworkCamera>();
	// 	camera.target = transform;

	// 	if (aimController != null) {
	// 		aimController.cam = cam.GetComponent<Camera>();
	// 	}
	// }

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}

		cam = Instantiate(cameraPrefab);
		testNetworkCamera camera = cam.GetComponent<testNetworkCamera>();
		camera.target = transform;

		if (aimController != null) {
			aimController.cam = cam.GetComponent<Camera>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (aimController != null) {
			aimController.cam = cam.GetComponent<Camera>();
		}
	}

	public Camera GetPlayerCamera() {
		// if (cam != null) {
			Camera camRef = cam.GetComponent<Camera>();
			return camRef;
		// }
	}
}
