using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour {

    public GameObject lobby;
    public GameObject mainCanvas;


    public void startGame() //Goes to the next scene. The Main Menu should have an index of one for this to load into the first level 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //lobby.SetActive(true);
        //mainCanvas.SetActive(false);
    }

    public void quitGame() //Quits the application
    {
        Debug.Log("You've quit the game");
        Application.Quit();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
