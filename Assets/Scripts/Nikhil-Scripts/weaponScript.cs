using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    //This script is for weapons. For now I am making it for the player fist, and then for the gun.
    //Maybe this can be used for the enemy attacks at well

    public int damage;
    public bool attacked;
    public BoxCollider box;



    //When the player attacks, the playerController will activate the trigger collider of the weapon. Which will hurt the enemy when it detects an enemy.

    public void attackActivate()
    {
        box.enabled = true;
        attacked = false;
    }

    public void attackDeactivate()
    {
        box.enabled = false;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(!attacked)
            {
                //Debug.Log("Attacking");
                other.GetComponent<enemyController>().hurtEnemy(damage); //This is where the damage is done
                attacked = true;
            }
            else
            {
               // Debug.Log("You've already been attacked this turn!");
            }   
        }
    }

    // Right now it is 
    void Start () {
        box = GetComponent<BoxCollider>();
        box.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
