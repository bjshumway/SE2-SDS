using UnityEngine;
using System.Collections;

public class OverworldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Map.tier = 1;
        //Map.generateMapByLevel();

    }

    //Switches to the battle scene
    //generates Monsters in combat based on the LevelSpecs class
    public void startBattle()
    {
        Monster[] monsters = Monster.genMonstersByLevel(Map.tier);
        BattleScript.instance.beginCombat(monsters);
        GameMaster.instance.switchCamera(5);
        Debug.Log(BattleScript.instance.monsters.Length);
        BattleScript.instance.monsters[0].image.transform.SetParent(GameObject.Find("BattleCanvas").transform, false);
    }
}
