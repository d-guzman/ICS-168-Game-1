using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testCharacterDirection : NetworkBehaviour {

	public Camera cam;

	public bool useNetworking = false;

	// Use this for initialization
	void Start () {
		if (useNetworking) {
			PlayerCameraSpawner camSpawner = gameObject.GetComponent<PlayerCameraSpawner>();
			if (camSpawner.CameraSpawned()) {
				cam = camSpawner.GetPlayerCamera();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!useNetworking) {
			FindPlayerDirection();
		}
		
		else {
			if (!isLocalPlayer) {
				return;
			}

			FindPlayerDirection();
		}
	}

	// helper function that calculates where the player object should be pointing based on player input.
	private void FindPlayerDirection() {
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
		}
	}

	// void FixedUpdate() {
	// 	RaycastHit hit;
	// 	Ray ray = cam.ScreenPointToRay(Input.mousePosition);
	// 	if (Physics.Raycast(ray, out hit)) {
	// 		transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
	// 	}
	// }
}
