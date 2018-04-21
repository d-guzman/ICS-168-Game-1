using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    private bool gameStarted;
    private bool gamePaused;

    // These should be references to any CountdownTimer prefab that exists in our scenes.
    private GameObject GM_cdTimer = null;
    private TimerScript GM_cdTimerScript = null;
    // These varables are going to serve as a sort of safe storage for any values that come from the Timer.
    private int GM_secondsLeft = 60;    // This is an amount of seconds that should only appear for the on the first level.
    private float GM_countdownRate = 1f;     //This should only be appearing for the first level.

    void Awake() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// <para>This function is called every time a new scene is loaded. Values that need to be transferred between scenes are transferred through here.</para>
    /// </summary>
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) {
        if (instance == this) {
            GM_cdTimer = GameObject.FindGameObjectWithTag("Timer");
            if (GM_cdTimer != null) { GM_cdTimerScript = GM_cdTimer.GetComponentInChildren<TimerScript>(); }

            if (GM_cdTimerScript != null)
            {
                GM_cdTimerScript.secondsLeft = GM_secondsLeft;  // When the GM enters a scene, take the timer in the scene and set its seconds left to the value in the GM.
                GM_cdTimerScript.countdownRate = GM_countdownRate;  // Do the same as above, but for the countdown rate.
            }
        }
    }

    void Update() {
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
