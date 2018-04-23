using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script will manage the players health, abilities, attacking, etc

public class playerController : MonoBehaviour {

    public int health = 100;
    public int punchDamage = 20;
    public int shootDamage = 10;

    public float punchTime = 0.0f;

    public GameObject Fist;
    public weaponScript fistWeapon;
    public GameObject Gun;
    


    public void punch()
    {
        Vector3 originalPos = Fist.transform.localPosition;
        Vector3 desiredPos = new Vector3(originalPos.x, originalPos.y, originalPos.z + 10f);
        Fist.transform.localPosition = Vector3.MoveTowards(originalPos, desiredPos, 20 * Time.deltaTime);
    }

    private IEnumerator punching()  //This is the script for the fist. Basically the first goes by a certain distance and tells the weaponScript to attack. It is an IEnumerator so the player can't spam the attack
    {
        if(punchTime <= Time.time)
        {
            punchTime = Time.time + 0.7f;
            Vector3 originalPos = Fist.transform.localPosition;
            Vector3 desiredPos = new Vector3(originalPos.x, originalPos.y, originalPos.z + 17f);
            fistWeapon.attackActivate();
            Fist.transform.localPosition = Vector3.MoveTowards(originalPos, desiredPos, 30 * Time.deltaTime);
            yield return new WaitForSeconds(0.4f);
            Fist.transform.localPosition = Vector3.MoveTowards(Fist.transform.localPosition, originalPos, 25 * Time.deltaTime);
            fistWeapon.attackDeactivate();
        }
    }

    // Use this for initialization
    void Start () {

        fistWeapon = Fist.GetComponent<weaponScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) //Left click to punch
        {
            StartCoroutine("punching");
        }

        if (Input.GetMouseButtonDown(1)) //Right click to shoot
            Debug.Log("Pressed secondary button.");

    }
}
