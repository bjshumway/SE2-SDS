using UnityEngine;
using System.Collections;

public class OverworldScript : MonoBehaviour {

    public static OverworldScript s_Instance;
    private bool isFirstPass;

    // Use this for initialization
    void Start() {
        isFirstPass = true;
    }

    public void load()
    {
        if(!isFirstPass) { 
            BGM.instance.setMusic(BGM.SongNames.menu);
            GameMaster.instance.switchCamera(4);
        } else
        {
            isFirstPass = false;
            GameMaster.instance.switchCamera(4);
        }

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
        ShopInventoryScript.instance.load();
    }
    
    //Used to test a new part member joining
    //Calls thePlayer.addPartyMember
    public void testNewPartyMemberJoining()
    {
        GameMaster.instance.thePlayer.addPartyMember();
    }

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static OverworldScript instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first OverworldScript object in the scene.
                s_Instance = FindObjectOfType(typeof(OverworldScript)) as OverworldScript;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("OverworldScript");
                s_Instance = obj.AddComponent(typeof(OverworldScript)) as OverworldScript;
                Debug.Log("Could not locate an OverworldScript object. OverworldScript was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }
}
