using UnityEngine;
using System.Collections;

public class OverworldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    //Switches to the battle scene
    public void startBattle()
    {
        Combat.instance.beginCombat();
        GameMaster.instance.switchCamera(5);
    }
}
