using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class nextScenePortal : NetworkBehaviour {

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public void OnTriggerEnter(Collider other)
    {
        Scene curr_scene = SceneManager.GetActiveScene();
        int curr_index = curr_scene.buildIndex + 1;
        if (other.tag == "Player") //When a portal encounters a player, send it to the next scene
        {
          NetworkManager.singleton.ServerChangeScene(NameFromIndex(curr_index));   
        }
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}


