using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

    //This is outdated, please do not use this. tristinenemyController is the current one

    public int health = 100;

    public float lookRadius = 10f;

    NavMeshAgent agent; 

    public void hurtEnemy(int damage) //When the fist or bullet detect the enemy, they will call this function to hurt the enemy
    {
        health -= damage;
        Debug.Log("Oh no! I, the enemy have taken " + damage + " damage and only have " + health + " health remaining!");

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(health <= 0)
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
