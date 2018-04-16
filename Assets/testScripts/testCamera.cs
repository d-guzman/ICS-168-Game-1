using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour {

    public Transform target;

    public float smoothSpeed;

    public Vector3 offset;


    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
