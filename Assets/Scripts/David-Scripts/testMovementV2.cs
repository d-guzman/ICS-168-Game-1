using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class testMovementV2 : NetworkBehaviour {

	public float speed;

    public NavMeshAgent agent;

    public bool useNetworking = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (!useNetworking) {       
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
        
        else {
            if (!isLocalPlayer) {
                return;
            }
            
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
}
