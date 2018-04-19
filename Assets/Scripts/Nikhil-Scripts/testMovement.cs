using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Here are some bareones WASD controls 
public class testMovement : MonoBehaviour {

    public float speed;

    public Camera cam;

    public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {


                
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        transform.Translate(speed * moveX * Time.deltaTime, 0f, speed * moveZ * Time.deltaTime);
        if (moveX != 0 || moveZ != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            agent.SetDestination(transform.position);
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
