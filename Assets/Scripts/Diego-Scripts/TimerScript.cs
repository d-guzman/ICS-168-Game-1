using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class TimerScript : NetworkBehaviour {
    [Tooltip("secondsLeft is the amount of seconds that this timer has left before it reaches 0.")]

    [SyncVar]
    public int secondsLeft = 60;
    [Tooltip("countdownRate is how long it takes before the timer decrements by 1 second. A value of 1 means the timer decrements at 1 second per second.")]
    public float countdownRate = 1f;
    [Tooltip("This is the text object in a canvas that shows the player how much time they have left. You shouldn't have to worry about this if you're using the prefab.")]
    public UnityEngine.UI.Text timerTextObj;

    private string constantTimerText = "Time Left: ";
    private bool countdownStarted = false;


    void Update () {
        if (!countdownStarted && secondsLeft != 0) { StartCoroutine(decrementTime()); }
        timerTextObj.text = constantTimerText + secondsLeft.ToString();
	}

    /// <summary>
    /// <para>Add some amount of seconds to the timer.</para>
    /// </summary>
    public void addTime(int seconds) {

        secondsLeft += seconds;
    }

    /// <summary>
    /// <para>Subtract some amount of seconds to the timer.</para>
    /// </summary>
    public void subtractTime(int seconds) {
        secondsLeft -= seconds;
    }

    /// <summary>
    /// <para>Adjust the rate at which the timer decrements seconds.</para>
    /// <para>A value of 1 sets the countdown rate to 1 seconds per second.</para>
    /// </summary>
    public void setCountdownRate(int rate) {
        countdownRate = rate;
    }

    // Private Functions
    IEnumerator decrementTime() {
        countdownStarted = true;
        yield return new WaitForSeconds(countdownRate);
        if (secondsLeft != 0) { secondsLeft--; }
        countdownStarted = false;
    }
    // MissionTime - Runtime = Timeleft
    // MissionTime + seconds = timeafterkilledenemy | missionTime - seconds = timeafterhit
}
