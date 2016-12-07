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

    private GameObject beatTheGameBox;

    //Whether we fought the boss during the fight.
    private bool foughtBoss;

    //Whether we've beaten the game
    private bool shownBeatTheGameBox;

    private static VictoryHandler s_Instance = null;

    public List<UserControllable> uCsToLevel = new List<UserControllable>();

    GameObject battlesFought;
    GameObject battlesUntilNextBoss;

	// Use this for initialization
	void Start () {
        state = vhState.inActive;
        battlesFought = GameObject.Find("BattlesFought");
        battlesUntilNextBoss = GameObject.Find("BattlesUntilBoss");
        beatTheGameBox = GameObject.Find("BeatTheGame");
        beatTheGameBox.SetActive(false);
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

        BGM.instance.setMusic(BGM.SongNames.victory);
        
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
                    if(monsters[i].isFinalBoss)
                    {
                        GameMaster.instance.thePlayer.beatTheGame = true;
                    }
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

        //remove buff for charge strenght and set health to full
        foreach (var partyMember in GameMaster.instance.thePlayer.theParty)
        {
            if (partyMember != null) {
                partyMember.stats["strength"].clearBuffs();
                partyMember.health.setValue(partyMember.health.maxValue);
            }
        }


        //Update the tier
        if (foughtBoss && !GameMaster.instance.thePlayer.beatTheGame)
        {
            Tier.tier++;
            Tier.difficulty = 1;
            GameMaster.instance.switchBackground(Tier.tier);
            string tierName = (Tier.tier == 2 ? "Infested Caves" : "Haunted Graveyard");
            GameObject[] gArr = GameObject.FindGameObjectsWithTag("TierName");
            for(int i = 0; i < gArr.Length;i++)
            {
                gArr[i].GetComponentInChildren<Text>().text = tierName;
            }
            Tier.numBattlesInTier = 0;
        }
        else
        {
            Tier.difficulty++;
            
        }

        GameMaster.instance.thePlayer.numBattlesFought += 1;
        battlesFought.GetComponent<Text>().text = GameMaster.instance.thePlayer.numBattlesFought.ToString();

        battlesUntilNextBoss.GetComponent<Text>().text = (3 - Tier.numBattlesInTier).ToString(); 

        //get rid of trailing comma
        itemsString = itemsString.Trim();
        itemsString = itemsString.Trim(',');
        tooHeavyString = tooHeavyString.Trim();
        tooHeavyString = tooHeavyString.Trim(',');

        //Show the loot
        goldEarned.text = "Gold Earned: " + gold;
        itemsEarned.text = itemsString;
        itemsEarned.text += (lootTooHeavy ? "\n" + tooHeavyString : "");

        //Give gold to the player
        GameMaster.instance.thePlayer.inventory.gold += gold;

        //Delete the monster's prefabs and monsters
        for (int i = 0; i < monsters.Length; i++)
        {
            DestroyImmediate(monsters[i].monsterPrefab);
            monsters[i] = null;
        }
        BattleScript.instance.monsters = null;
        monsters = null;
        Monster.id_increment = 1;

        //Show the - you beat the game box!
        if (GameMaster.instance.thePlayer.beatTheGame && !shownBeatTheGameBox)
        {
            shownBeatTheGameBox = true;
            beatTheGameBox.SetActive(true);
        }

        //Show the victory box
        victoryBox.SetActive(true);

        //Await player's input
        state = vhState.awaitingInput;

    }

    public void clickRetire()
    {
        Application.Quit();
    }

    public void clickContinue()
    {
        beatTheGameBox.SetActive(false);
    }

    private void handleInput()
    {
        if(Input.anyKey)
        {
            if(beatTheGameBox.activeSelf)
            {
                return;
            }

            //still here?
            victoryBox.SetActive(false);
            if (foughtBoss && !GameMaster.instance.thePlayer.beatTheGame)
            {
                state = vhState.addingPartyMember;
                GameMaster.instance.thePlayer.addPartyMember();
            }
            else
            {
                state = vhState.levelingUCs;
                UserControllable uC = getNextUCToLevel();
                uC.levelUp();
                AbilitySelectionScript.instance.load(uC);
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

