using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// This script creates a camera for the networked version of the player.
// Do not use with offline version (yet).
public class PlayerCameraSpawner : NetworkBehaviour {

	public GameObject cameraPrefab;
	public testCharacterDirection aimController;

	private GameObject cam;

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
		// if (!isLocalPlayer) {
		// 	return;
		// }

		// if (cam == null) {
		// 	cam = Instantiate(cameraPrefab);
		// 	testNetworkCamera camera = cam.GetComponent<testNetworkCamera>();
		// 	camera.target = transform;

		// 	if (aimController != null) {
		// 		aimController.cam = cam.GetComponent<Camera>();
		// 	}
		// }
	}

	public override void OnStartLocalPlayer() {
		if (cam == null) {
			cam = Instantiate(cameraPrefab);
			testNetworkCamera camera = cam.GetComponent<testNetworkCamera>();
			camera.target = transform;

			if (aimController != null) {
				aimController.cam = cam.GetComponent<Camera>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			if (aimController != null) {
				aimController.cam = cam.GetComponent<Camera>();
			}
		}
	}

	public Camera GetPlayerCamera() {
		// if (cam != null) {
			Camera camRef = cam.GetComponent<Camera>();
			return camRef;
		// }
	}

	public bool CameraSpawned() {
		return (cam != null);
	}
}
