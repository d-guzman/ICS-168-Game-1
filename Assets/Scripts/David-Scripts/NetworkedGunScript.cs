using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedGunScript : NetworkBehaviour {

	public GameObject bullet;

    public Transform shootPoint;

    public int damage; //The gun dictates the damage



    [Command]
	public void CmdShoot()
    {
        //Debug.Log("BANG");
        GameObject bulletInstance =   Instantiate(bullet, shootPoint.position, transform.rotation);
        bulletScript bulletScriptInstance = bulletInstance.GetComponent<bulletScript>(); //The bullet has a default damage, but here the gun overrides it 
        bulletScriptInstance.damage = damage;
		NetworkServer.Spawn(bulletInstance);
    }




    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
