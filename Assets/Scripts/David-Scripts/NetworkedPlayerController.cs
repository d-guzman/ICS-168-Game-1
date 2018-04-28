using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerController : NetworkBehaviour {

	public int health = 100;
    public int punchDamage = 20;
    public int shootDamage = 10;

    public GameObject bullet;

    public Transform shootPoint;

   

    private float punchTime;
    private float shootTime;

    public GameObject Fist;
    public weaponScript fistWeapon;
    //public GameObject Gun;
    //public GameObject bullet;

    // public NetworkedGunScript gun;

    //public weaponScript gunWeapon;
    


    [Command]
    void CmdStartPunch()
    {
        if(punchTime <= Time.time)
        {
            punchTime = Time.time + 0.7f;
            Vector3 originalPos = Fist.transform.localPosition;
            Vector3 desiredPos = new Vector3(originalPos.x, originalPos.y, originalPos.z + 17f);
            fistWeapon.attackActivate();
            Fist.transform.localPosition = Vector3.MoveTowards(originalPos, desiredPos, 25 * Time.deltaTime);
            StartCoroutine("pausePunchAnimation", originalPos);
            // Fist.transform.localPosition = Vector3.MoveTowards(Fist.transform.localPosition, originalPos, 25 * Time.deltaTime);
            // fistWeapon.attackDeactivate();
        }
        // Vector3 originalPos = Fist.transform.localPosition;
        // Vector3 desiredPos = new Vector3(originalPos.x, originalPos.y, originalPos.z + 10f);
        // Fist.transform.localPosition = Vector3.MoveTowards(originalPos, desiredPos, 20 * Time.deltaTime);
    }

    [Command]
    void CmdFinishPunch(Vector3 originalPos) {
        Fist.transform.localPosition = Vector3.MoveTowards(Fist.transform.localPosition, originalPos, 25 * Time.deltaTime);
        fistWeapon.attackDeactivate();
    }

    private IEnumerator pausePunchAnimation(Vector3 originalPos) {
        yield return new WaitForSeconds(0.4f);
        CmdFinishPunch(originalPos);
    }

    private IEnumerator punching()  //This is the script for the fist. Basically the first goes by a certain distance and tells the weaponScript to attack. It is an IEnumerator so the player can't spam the attack
    {
        if(punchTime <= Time.time)
        {
            punchTime = Time.time + 0.6f;
            Vector3 originalPos = Fist.transform.localPosition;
            Vector3 desiredPos = new Vector3(originalPos.x, originalPos.y, originalPos.z + 0.7f);
            fistWeapon.attackActivate();
            Fist.transform.localPosition = desiredPos; //Vector3.MoveTowards(originalPos, desiredPos, 25 * Time.deltaTime);
            yield return new WaitForSeconds(0.4f);
            Fist.transform.localPosition = originalPos;  //Vector3.MoveTowards(Fist.transform.localPosition, originalPos, 25 * Time.deltaTime);
            fistWeapon.attackDeactivate();
        }
    }

    [Command]
    void CmdShooting() {
        if(shootTime <= Time.time)
        {
            shootTime = Time.time + 0.3f;

            CmdShoot();
            //Vector3 spot = transform.position;

            //GameObject shotBullet = Instantiate(bullet, spot, transform.rotation);
            //weaponScript bulletReference = shotBullet.GetComponent<weaponScript>();
            //bulletReference.attackActivate();
            // yield return new WaitForSeconds(0.1f);
        }
    }

    [Command]
	public void CmdShoot()
    {
        //Debug.Log("BANG");
        GameObject bulletInstance =   Instantiate(bullet, shootPoint.position, transform.rotation);
        bulletScript bulletScriptInstance = bulletInstance.GetComponent<bulletScript>(); //The bullet has a default damage, but here the gun overrides it 
        bulletScriptInstance.damage = shootDamage;
		NetworkServer.Spawn(bulletInstance);
    }

    // [Command]
    // private IEnumerator shooting() //This is the script 
    // {
    //     if(shootTime <= Time.time)
    //     {
    //         shootTime = Time.time + 0.3f;

    //         gun.shoot();
    //         //Vector3 spot = transform.position;

    //         //GameObject shotBullet = Instantiate(bullet, spot, transform.rotation);
    //         //weaponScript bulletReference = shotBullet.GetComponent<weaponScript>();
    //         //bulletReference.attackActivate();
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }
    
    // Use this for initialization
    void Start () {

        fistWeapon = Fist.GetComponent<weaponScript>();
        punchTime = 0.0f;
        shootTime = 0.0f;
        //gunWeapon = Gun.GetComponent<weaponScript>();
		
	}

	// public override void OnStartLocalPlayer() {
	// 	fistWeapon = Fist.GetComponent<weaponScript>();
	// }
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

        if (Input.GetMouseButtonDown(0)) //Left click to punch
        {
            // CmdStartPunch();
            StartCoroutine("punching");
        }

        if (Input.GetMouseButtonDown(1)) //Right click to shoot
        {
            //Debug.Log("Shooting is going on");
            // StartCoroutine("shooting");
            CmdShooting();
        }
            

    }
}
