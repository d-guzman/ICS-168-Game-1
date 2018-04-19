using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCharacterDirection : MonoBehaviour {

	public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
