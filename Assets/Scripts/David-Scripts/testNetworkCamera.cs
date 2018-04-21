using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Networked version of Nikhil's testCamera class
public class testNetworkCamera : NetworkBehaviour {

	public Transform target;

    public float smoothSpeed;

    public Vector3 offset;

	// Use this for initialization
    void Start () {
		
	}

    private void LateUpdate()
    {
		// if (!isLocalPlayer) {
		// 	return;
		// }
		
        if (target != null) {
            transform.position = target.transform.position + offset;
            transform.LookAt(target);
        }
    }
}
