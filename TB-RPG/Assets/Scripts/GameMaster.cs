using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

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
    //
    public void switchCamera(string sceneName)
    {

    }
}
