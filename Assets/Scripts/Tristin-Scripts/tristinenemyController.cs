using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using UnityEngine.UI;

public class tristinenemyController : NetworkBehaviour {
    bool engaged = false;
    float wepRange = 1;
    float engageDist = 8;
    public GameObject[] players;
    public GameObject target_player;

    public int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int health;

    public float lookRadius = 10f;

    public float attackTime = 0.0f; //If attackTime <= Time.time, then attack. During attack, make attackTime = Time.time + attackWaitTime
    public float attackWaitTime = 0.5f; //this basically means the enemy can attack every attackWaitTime seconds 

    public int timeDeathValue = 4; //time the player gets on enemy death
    public int timeAttackValue = 2; //time the player loses on attack

    public int lastAttackWas = 0; //this indicates what the last attack was. Will be used to award time points


    public RectTransform healthBar; 

    NavMeshAgent agent;

    GameManager gameManagerReference;
    

    public void hurtEnemy(int damage) //When the fist or bullet detect the enemy, they will call this function to hurt the enemy
    {
        if (!isServer) {
            return;
        }

        health -= damage;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        //StartCoroutine("knockBack");
        //Debug.Log("Oh no! I, the enemy, have taken " + damage + " damage and only have " + health + " health remaining!");
    }

    public void bulletKnockback()
    {
        StartCoroutine("knockBack", 2);
    }

    public void punchKnockback()
    {
        StartCoroutine("knockBack", 4);
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    public IEnumerator knockBack(int tim)
    {
        //Debug.Log("Knock back for "+tim);

        agent.velocity = -agent.velocity;
        yield return new WaitForSeconds(0.2f);
        agent.velocity = -agent.velocity;
    }

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player"); //Makes a list of all player objects at start
        gameManagerReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        // dont let clients execute enemy updates
        // if (!isServer) {
        //     return;
        // }

        if (players.Length < 2) {
            players = GameObject.FindGameObjectsWithTag("Player"); //Makes a list of all player objects at start
        }

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
            // RpcSetEnemyDestination();
        }

        checkDeath();

	}

    // Makes sure the client is updated from server (?)
    [ClientRpc]
    void RpcSetEnemyDestination() {
        GetComponent<NavMeshAgent>().destination = target_player.transform.position;
    }

    public void checkDeath()
    {

        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            if(lastAttackWas == 1 )
            {
                timeDeathValue = timeDeathValue * 2;
            }
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
