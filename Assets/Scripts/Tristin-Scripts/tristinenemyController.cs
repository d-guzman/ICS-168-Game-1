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

    public float attackTime = 0.0f; //If attackTime <= Time.time, then attack. During attack, make attackTime = Time.time + attackWaitTime
    public float attackWaitTime = 0.5f; //this basically means the enemy can attack every attackWaitTime seconds 

    public int timeDeathValue = 5; //time the player gets on enemy death
    public int timeAttackValue = 2; //time the player loses on attack

    NavMeshAgent agent;

    GameManager gameManagerReference;
    

    public void hurtEnemy(int damage) //When the fist or bullet detect the enemy, they will call this function to hurt the enemy
    {
        health -= damage;
        //Debug.Log("Oh no! I, the enemy, have taken " + damage + " damage and only have " + health + " health remaining!");

    }

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player"); //Makes a list of all player objects at start
        gameManagerReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        {
            if(attackTime <= Time.time)
            {
                attackTime = Time.time + attackWaitTime; //this means you can attack every attackWaitTime seconds
                gameManagerReference.subtractSomeTime(timeAttackValue); //this is how the player loses time
            }
        }
           
        else if (minDist <= engageDist || engaged) //Checks to see if Enemy is in range of closest player
        {
            engaged = true;
            GetComponent<NavMeshAgent>().destination = target_player.transform.position;
        }

        checkDeath();

	}

    public void checkDeath()
    {

        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            gameManagerReference.addSomeTime(timeDeathValue);
            Destroy(this.gameObject);
        }

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
