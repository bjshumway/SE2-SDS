using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class VictoryHandler : MonoBehaviour {

    public enum vhState 
    {
        inActive,
        waitingForAnimationsToFinish,
        displayingVictoryText,
        awaitingInput,
        addingPartyMember,
        levelingUCs
    }

    //The amount of time (seconds) you need to wait for animations to finish
    public float waitTime = 3;

    private float startTime;

    public vhState state;

    private Monster[] monsters;

    private GameObject victoryBox;
    private Text goldEarned;
    private Text itemsEarned;

    //Whether we fought the boss during the fight.
    private bool foughtBoss;

    private static VictoryHandler s_Instance = null;

    public List<UserControllable> uCsToLevel = new List<UserControllable>(); 

	// Use this for initialization
	void Start () {
        state = vhState.inActive;
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case vhState.inActive:
                //do nothing
                return;
            case vhState.waitingForAnimationsToFinish:
                if(Time.realtimeSinceStartup > startTime + waitTime)
                {
                    state = vhState.displayingVictoryText;
                }
                break;
            case vhState.displayingVictoryText:
                showResults();
                break;
            case vhState.awaitingInput:
                handleInput();
                break;
            case vhState.addingPartyMember:
                break;
            case vhState.levelingUCs:
                break;
        }
	}


    public void beginVictory(Monster[] monsters, GameObject victoryBox)
    {
        state = vhState.inActive;
        this.monsters = monsters;
        startTime = Time.realtimeSinceStartup;


        BattleScript.instance.pipeInputFunc = null;

        if (this.victoryBox == null)
        {
            this.victoryBox = victoryBox;
            goldEarned = victoryBox.GetComponentsInChildren<Text>()[0];
            itemsEarned = victoryBox.GetComponentsInChildren<Text>()[1];
        }
        this.state = vhState.waitingForAnimationsToFinish;
        
        for(int i = 0; i< GameMaster.instance.thePlayer.theParty.Length; i++)
        {
            if (GameMaster.instance.thePlayer.theParty[i] != null) {
                uCsToLevel.Add(GameMaster.instance.thePlayer.theParty[i]);
            }
        }
    }

    public UserControllable getNextUCToLevel()
    {
        if(uCsToLevel.Count > 0)
        {
            state = vhState.levelingUCs;
            UserControllable u = uCsToLevel[0];
            uCsToLevel.RemoveAt(0);
            return u;
        } else
        {
            state = vhState.inActive;
            return null;
        }
    }

    private void showResults()
    {

        //Give the loot to the player!
        decimal gold = 0;
        string itemsString = "Loot: ";
        foughtBoss = false;

        bool lootTooHeavy = false;
        string tooHeavyString = "Too heavy to pickup: ";
        for (int i = 0; i < monsters.Length; i++)
        {
            if(monsters[i] != null)
            {
                gold += monsters[i].goldDrop;
                if (monsters[i].isBoss)
                {
                    foughtBoss = true;
                }
            }

            bool added = GameMaster.instance.thePlayer.inventory.addItem(monsters[i].itemDrop);

            if (!added)
            {
                lootTooHeavy = true;
                tooHeavyString += monsters[i].itemDrop + ", ";
                monsters[i].itemDrop = null;
            }
            else
            {
                itemsString += monsters[i].itemDrop + ", ";

                //Place item in the inventory
                GameMaster.instance.thePlayer.inventory.addItem(monsters[i].itemDrop);
            }
        }

        // remove Iron Skin if it's there
        foreach (var partyMember in GameMaster.instance.thePlayer.theParty) {
            if (partyMember != null) {
                for (int x = partyMember.passiveAbilities.Count - 1; x > -1; x--) {
                    if (partyMember.passiveAbilities[x].name == "Iron Skin") {
                        partyMember.passiveAbilities.RemoveAt(x);
                    }
                }
            }
        }

        //Update the tier
        if (foughtBoss)
        {
            Tier.tier++;
        }
        else
        {
            Tier.difficulty++;
        }

        //get rid of trailing comma
        itemsString = itemsString.Trim();
        itemsString = itemsString.Trim(',');
        tooHeavyString = tooHeavyString.Trim();
        tooHeavyString = tooHeavyString.Trim(',');

        //Show the loot
        goldEarned.text = "Gold Earned: " + gold;
        itemsEarned.text = itemsString;
        itemsEarned.text += (lootTooHeavy ? "\n" + tooHeavyString : "");

        //Delete the monster's prefabs and monsters
        for (int i = 0; i < monsters.Length; i++)
        {
            DestroyImmediate(monsters[i].monsterPrefab);
            monsters[i] = null;
        }
        BattleScript.instance.monsters = null;
        monsters = null;
        Monster.id_increment = 1;

        //Show the victory box
        victoryBox.SetActive(true);

        //Await player's input
        state = vhState.awaitingInput;

    }

    private void handleInput()
    {
        if(Input.anyKey)
        {
            victoryBox.SetActive(false);
            if (foughtBoss)
            {
                state = vhState.addingPartyMember;
                GameMaster.instance.thePlayer.addPartyMember();
            }
            else
            {
                state = vhState.levelingUCs;
                AbilitySelectionScript.load(getNextUCToLevel());
            }
        }
    }



    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static VictoryHandler instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first VictoryHandler object in the scene.
                s_Instance = FindObjectOfType(typeof(VictoryHandler)) as VictoryHandler;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("VictoryHandler");
                s_Instance = obj.AddComponent(typeof(VictoryHandler)) as VictoryHandler;
                //Debug.Log("Could not locate an BattleScript object. BattleScript was Generated Automaticly.");
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

