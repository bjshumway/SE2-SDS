using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public enum playerStates {
        menuScreen,
        doingBattle,
        outsideBattle,
        characterConfiguration
    };

    public static playerStates playerState = playerStates.wondering;

    public ActorNS.Player thePlayer;
    public Map theMap;

	// Use this for initialization
	void Start () {
        thePlayer = new ActorNS.Player("Monkey", 7, 7, null, null, null);
        theMap = new Map(1);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
