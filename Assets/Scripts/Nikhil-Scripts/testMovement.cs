using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Here are some bareones WASD controls 
public class testMovement : MonoBehaviour {

    public float speed;

    public Camera cam;

    public NavMeshAgent agent;

    public bool movementAllowed;

	// Use this for initialization
	void Start () {
        movementAllowed = true;
	}

    public void disableMovement()
    {
        Debug.Log("Movement disabled");
        movementAllowed = false;
    }
        //Other scripts can call this to prevent the player from moving while events, such as teleportation, are going on
    public void enableMovement()
    {
        Debug.Log("Movement enabled");
        movementAllowed = true;
    }

    public void teleportTo(Vector3 location)
    {
        Debug.Log("The player is at " + transform.position);
        Debug.Log("The player should move to " + location);
        //Vector3.Lerp(transform.position, location, 1f);
        //transform.position = location;
        agent.Warp(location);
        Debug.Log("NOW player is at " + transform.position);

    }

    // Update is called once per frame
    void Update() {


                
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        if(movementAllowed)
        {
            transform.Translate(speed * moveX * Time.deltaTime, 0f, speed * moveZ * Time.deltaTime);
            if (moveX != 0 || moveZ != 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                agent.SetDestination(transform.position);
            }
        }

       
            
     
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {       //Now the agent is moved
                agent.SetDestination(hit.point);


            }
        }
    }
}
