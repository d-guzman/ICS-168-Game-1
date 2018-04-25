using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    public GameObject bullet;

    public Transform shootPoint;

    public int damage; //The gun dictates the damage



    public void shoot()
    {
        Debug.Log("BANG");
         GameObject bulletInstance =   Instantiate(bullet, shootPoint.position, transform.rotation);
        bulletScript bulletScriptInstance = bulletInstance.GetComponent<bulletScript>(); //The bullet has a default damage, but here the gun overrides it 
        bulletScriptInstance.damage = damage;
    }




    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
