using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
    public static GameManager instance = null;

    private bool gameStarted;
    private bool gamePaused;

    //public GameObject[] enemies;

    public int enemyCount;

    public GameObject portal;

    public Transform enemyDeathSpot;

    public Vector3 enemyDeathPosition;
    public Quaternion enemyDeathRoation;

    public bool enemyKilled = false;

    public bool portalExists = false;
   

    // These should be references to any CountdownTimer prefab that exists in our scenes.
    public GameObject GM_cdTimer = null;
    public TimerScript GM_cdTimerScript = null;
    // These varables are going to serve as a sort of safe storage for any values that come from the Timer.

    [SyncVar]
    public int GM_secondsLeft = 60;    // This is an amount of seconds that should only appear for the on the first level.
    private float GM_countdownRate = 1f;     //This should only be appearing for the first level.

    void Awake() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

        ///enemies = GameObject.FindGameObjectsWithTag("Enemy"); //Makes a list of all player objects at start
    }


    

    /// <summary>
    /// <para>This function is called every time a new scene is loaded. Values that need to be transferred between scenes are transferred through here.</para>
    /// </summary>
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) {
        Debug.Log("game manager transferring data btw scenes.");
            enemyKilled = false;
            portalExists = false;
        if (instance == this) {
            Debug.Log("instance is correct.");
            GM_cdTimer = GameObject.FindGameObjectWithTag("Timer");
            if (GM_cdTimer != null) { GM_cdTimerScript = GM_cdTimer.GetComponentInChildren<TimerScript>(); }

            if (GM_cdTimerScript != null)
            {
                GM_cdTimerScript.secondsLeft = GM_secondsLeft;  // When the GM enters a scene, take the timer in the scene and set its seconds left to the value in the GM.
                GM_cdTimerScript.countdownRate = GM_countdownRate;  // Do the same as above, but for the countdown rate.
            }
        }
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("WHEN DOES THIS HAPPPEN");
    }

    


    public void lastEnemyKilled(Transform enemyTransform)
    {
        //enemyDeathSpot = enemyTransform;
        enemyKilled = true;
        enemyDeathPosition = new Vector3(enemyTransform.position.x, enemyTransform.position.y, enemyTransform.position.z);
        enemyDeathRoation = enemyTransform.rotation;
        
        //Debug.Log("The enemyDeathSpot is " + enemyDeathSpot.position);
    }

    public void addSomeTime(int num)
    {
        if (GM_cdTimerScript != null)
            GM_cdTimerScript.addTime(num);
    }

    public void subtractSomeTime(int num)
    {
        if (GM_cdTimerScript != null)
            GM_cdTimerScript.subtractTime(num);
    }

    void Update() {
        Debug.Log("game manager updating.");
        if (GM_cdTimer == null) {
            GM_cdTimer = GameObject.FindGameObjectWithTag("Timer");
        }

        if (GM_cdTimerScript == null && GM_cdTimer != null) {
            GM_cdTimerScript = GM_cdTimer.GetComponentInChildren<TimerScript>();
        }

        if (!isServer) {
            return;
        }

        RpcUpdateTimerValues();


        if(enemyCount <= 0)
        {
            //Debug.Log("Yup this is going on!"+enemyDeathSpot.position);
            if(portalExists == false)
            {
                if (enemyKilled)
                {
                    GameObject portalInstance = Instantiate(portal, enemyDeathPosition, enemyDeathRoation);
                    portalExists = true;
                    NetworkServer.Spawn(portalInstance);
                }
            }
           
        }
    }

    [ClientRpc]
    private void RpcUpdateTimerValues() {
        updateTimerValues();
    }

    private void updateTimerValues() {
        if (GM_cdTimerScript != null)
        {
            GM_secondsLeft = GM_cdTimerScript.secondsLeft;
            GM_countdownRate = GM_cdTimerScript.countdownRate;
        }
    }
}
