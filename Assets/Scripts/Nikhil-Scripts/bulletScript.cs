using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bulletScript : NetworkBehaviour {

    public float speed = 30f;

    public Vector3 travel;

    public int damage = 12;

    // Use this for initialization
    void Start () {
        travel = Vector3.forward;
        Destroy(this.gameObject, 5f); //bullets kill themselves after 5 minutes
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<tristinenemyController>().lastAttackWas = 2; //this means that the latest attack was a bullet
            other.GetComponent<tristinenemyController>().hurtEnemy(damage); //This is where the damage is done
            other.GetComponent<tristinenemyController>().bulletKnockback();
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Wall") {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        transform.Translate(travel * Time.deltaTime * speed);
    }
}
