using UnityEngine;
using System.Collections;

public class OverworldScript : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    //Switches to the battle scene
    //generates Monsters in combat based on the LevelSpecs class
    public void startBattle()
    {
        Monster[] monsters = Monster.genMonstersByLevel(Tier.tier, Tier.difficulty);
        Tier.numBattlesInTier++;

        BattleScript.instance.beginCombat(monsters);
        GameMaster.instance.switchCamera(5);
        //Debug.Log(BattleScript.instance.monsters.Length);
    }

    //Goes to the menu screen
    public void goToMenu()
    {
        GameMaster.instance.switchCamera(6);
    }
    
    //Used to test a new part member joining
    //Calls thePlayer.addPartyMember
    public void testNewPartyMemberJoining()
    {
        GameMaster.instance.thePlayer.addPartyMember();
    }
}
