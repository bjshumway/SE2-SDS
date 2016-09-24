using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public Camera[] theCameras;


    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameMaster s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static GameMaster instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(GameMaster)) as GameMaster;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GameMaster");
                s_Instance = obj.AddComponent(typeof(GameMaster)) as GameMaster;
                Debug.Log("Could not locate an GameMaster object. GameMaster was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }


    public enum playerStates {
        menuScreen,
        doingBattle,
        outsideBattle,
        characterConfiguration
    };

    public static playerStates playerState = playerStates.outsideBattle;

    public Player thePlayer;
    public Map theMap;

	// Use this for initialization
	void Start () {
        thePlayer = new Player("Monkey", 7, 7, null, null, null);
        Debug.Log("no hands!");
        //theMap = new Map
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    //Switches to the new camera
    public void switchCamera(string cameraNumber)
    {
        int camNum = int.Parse(cameraNumber);
        for(int i = 0; i < theCameras.Length; i++)
        {
            theCameras[i].enabled = false;
        }
        theCameras[camNum].enabled = true;

    }
}
