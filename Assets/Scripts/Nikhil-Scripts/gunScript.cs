using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    public GameObject bullet;

    public Transform shootPoint;

    public void shoot()
    {
        Debug.Log("BANG");
        Instantiate(bullet, shootPoint.position, transform.rotation);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
