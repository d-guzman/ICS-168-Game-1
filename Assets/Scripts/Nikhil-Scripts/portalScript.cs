using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour {

    //This will send the player to another portal when it comes in contact with a player


    //Every portal needs a reference to a destination. This is the place where portal teleports to. 
    public portalScript destination;

    public bool receiving;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && receiving==false) //When a portal encounters a player, uses the NavMesh Warp function to move it to the transport.position of the destination portal
        {
            destination.receiving = true;
            
            testMovementV3 playerMove = other.GetComponent<testMovementV3>();
            playerMove.disableMovement();
            playerMove.teleportTo(destination.transform.position);
            playerMove.enableMovement();
        }

        if (other.tag == "Player" && receiving == true)
        {
            //Debug.Log("The player has been received at : " + transform.position+" by a portal");
        }
    }

    public void OnTriggerExit(Collider other) //When a player leaves it becomes ready to teleport again 
    {
        if(other.tag=="Player")
        {
            //Debug.Log("The player is going away so receiving is now false");
            receiving = false;
        }
        
    }



    // Use this for initialization
    void Start () {

        receiving = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
