using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tristinenemyController : MonoBehaviour {
    bool engaged = false;
    float wepRange = 1;
    float engageDist = 8;
    public GameObject[] players;
    public GameObject target_player;

    public int health = 100;

    public float lookRadius = 10f;

    NavMeshAgent agent; 

    public void hurtEnemy(int damage) //When the fist or bullet detect the enemy, they will call this function to hurt the enemy
    {
        health -= damage;
        Debug.Log("Oh no! I, the enemy, have taken " + damage + " damage and only have " + health + " health remaining!");

    }

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player"); //Makes a list of all player objects at start

    }
	
	// Update is called once per frame
	void Update () {
        float minDist = Mathf.Infinity;                //The distance from the enemy to the closest player
        Vector3 currentPos = transform.position;
        foreach (GameObject t in players)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                target_player = t;
                minDist = dist;
            }
        }
        if (minDist <= wepRange)
            Debug.Log("Enemy attacking Player");
        else if (minDist <= engageDist || engaged) //Checks to see if Enemy is in range of closest player
        {
            engaged = true;
            GetComponent<NavMeshAgent>().destination = target_player.transform.position;
        }
        
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(this.gameObject);
        }
		
	}

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
