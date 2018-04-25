using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float speed = 10f;

    public Vector3 travel;

    public int damage;

    // Use this for initialization
    void Start () {
        travel = Vector3.forward;
        Destroy(this.gameObject, 5f); //bullets kill themselves after 5 minutes
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<enemyController>().hurtEnemy(damage); //This is where the damage is done
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
