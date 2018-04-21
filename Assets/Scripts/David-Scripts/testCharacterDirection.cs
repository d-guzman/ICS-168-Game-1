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
			if (camSpawner.cam != null) {
				cam = camSpawner.GetPlayerCamera();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!useNetworking) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
			}
		}
		
		else {
			if (!isLocalPlayer) {
				return;
			}

			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
			}
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
