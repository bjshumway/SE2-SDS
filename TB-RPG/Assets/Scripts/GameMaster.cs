using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public enum playerStates {
        menuScreen,
        doingBattle,
        wondering
    };

    public static playerStates playerState = playerStates.wondering;
    



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
