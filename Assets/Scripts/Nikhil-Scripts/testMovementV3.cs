using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class testMovementV3 : NetworkBehaviour
{

    public float speed;

    public NavMeshAgent agent;

    public bool useNetworking = false;

    public bool movementAllowed = true;

    // Use this for initialization
    void Start()
    {

    }

    public void disableMovement()
    {
        //Debug.Log("Movement disabled");
        movementAllowed = false;
    }
    //Other scripts can call this to prevent the player from moving while events, such as teleportation, are going on
    public void enableMovement()
    {
        //Debug.Log("Movement enabled");
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
    void Update()
    {
        if (!useNetworking)
        {
            CalculatePlayerMovement();
        }

        else
        {
            if (!isLocalPlayer)
            {
                return;
            }

            CalculatePlayerMovement();
        }

        // if (Input.GetMouseButton(0))
        // {
        //     RaycastHit hit;
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //     if (Physics.Raycast(ray, out hit))
        //     {       //Now the agent is moved
        //         agent.SetDestination(hit.point);


        //     }
        // }
    }

    private void CalculatePlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movVector = new Vector3(moveX, 0f, moveZ);
        movVector.Normalize();
        
        
            transform.Translate(movVector * speed * Time.deltaTime, Space.World);
            if (moveX != 0 || moveZ != 0)
            {
                // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                agent.SetDestination(transform.position);
            }
        


    }
}
