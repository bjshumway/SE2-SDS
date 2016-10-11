using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BattleScript : MonoBehaviour {
    public bool combatOcurring;
    public bool isPaused;

    public GameObject DemonSkull;

    public Monster[] monsters;

    public UserControllable activeCharacter;

    public System.Random random;

    //This function is used when a class (e.g. ability) wants to intercept all mouse / keyboard input
    //When it is null, mouse/keyboard input does its default thing
    public Action<string> pipeInputFunc;

    //Using KeyCode lets you map an action to a key on the keyboard
    public KeyCode attack;
    public KeyCode item;

    //Combat.cs is a singleton (static class). Use Combat.instance to get access to it.
    private static BattleScript s_Instance = null;

    // Use this for initialization
    void Start () {  
        //Debug.Log("inside combat start");
        random = new System.Random();


        GameObject.Find("Battle UC 2 HeadType").GetComponent<Image>().enabled = false;
        GameObject.Find("Battle UC 2 HealthBar").SetActive(false);// = false;
        GameObject.Find("Battle UC 2 StaminaBar").SetActive(false);

        GameObject.Find("Battle UC 3 HeadType").GetComponent<Image>().enabled = false;
        GameObject.Find("Battle UC 3 HealthBar").SetActive(false);
        GameObject.Find("Battle UC 3 StaminaBar").SetActive(false);

    }






    //Todo: update this function so that it takes an array of monsters as an argument
    public void beginCombat(Monster[] monsters)
    {
        this.combatOcurring = true;
        this.monsters = monsters;

        //place each monster's GUI related stuff onto the battle field

        //Set all players and monster's stamina's to a random amount
        UserControllable[] theParty = GameMaster.instance.thePlayer.theParty;
        for (int i = 0; i < theParty.Length; i++)
        {
            if(theParty[i] != null)
            {
                Resource stamina = theParty[i].stamina;
                stamina.setValue(random.Next((int)stamina.value));
                theParty[i].battleStaminaBar.value = (float)stamina.value;
            }
        }
        for(int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] != null)
            {
                Resource stamina = monsters[i].stamina;
                stamina.setValue(random.Next((int)stamina.value));
                monsters[i].battleStaminaBar.value = (float)stamina.value;

            }
        }


        //Set thePlayer as the initial player that's active.
        setActiveUC("UserControllable 0");
    }

    //Sets the active UC and populates the menu bar
    //Called from beginCombat, and from when the user clicks on one of the character portraits
    public void setActiveUC(string arg)
    {
        if(pipeInputFunc != null)
        {
            pipeInputFunc(arg);
            return;
        }
        int uCNum = int.Parse(arg.Split()[1]);
        UserControllable uC = GameMaster.instance.thePlayer.theParty[uCNum]; 
        if(uC == null)
        {
            return;
        }
        activeCharacter = uC;
        //uC.damage(1);
        //uC.battleHealthBar.value = (float) uC.health.value;
        
        //populate the menu bar
        AbilityBar abs = activeCharacter.abilities;
        for (int i = 0; i < abs.abilities.Length; i++)
        {
            Ability ab = abs.abilities[i];
            if (ab != null)
            {
                GameObject.Find("AbSlot" + (i + 1) + "_Name").GetComponent<Text>().text = ab.name;
                GameObject.Find("AbSlot" + (i + 1) + "_Cost").GetComponent<Text>().text = "" + ab.stamina;
            }
        }

    }

    //Calls arg.split()
    //Arg 0 is the type of object clicked on
    //Arg 1 is the index of the object clicked on
    //Casts the ability of the active character
    //E.g. "AbilityButton 0" will call the 0th indexed ability in the current active plaer.  
    public void abilityButtonClick(string arg)
    {
        if(pipeInputFunc != null)
        {
            pipeInputFunc(arg);
            return;
        }

        int abNum = int.Parse(arg.Split()[1]);
        Ability ab = activeCharacter.abilities.abilities[abNum];
        if(ab != null && ab.stamina <= activeCharacter.stamina.value)
        {
            ab.cast();
        }
    }

    //Calls arg.split()
    //Arg 0 is the type of object clicked on
    //Arg 1 is the index of the object clicked on
    //Does the item ability
    public void itemButtonClick(string arg)
    {
        if (pipeInputFunc != null)
        {
            pipeInputFunc(arg);
            return;
        }
        //will the item button pull up all items? 
        //Or will it be like a slot, so it will contain the item chosen for that slot
    }

    //Calls arg.split()
    //Arg 0 is the type of object clicked on
    //Arg 1 is the index of the object clicked on
    //Pauses the battle game, and brings up the menu
    public void menuButtonClick(string arg)
    {
        if (pipeInputFunc != null)
        {
            pipeInputFunc(arg);
            return;
        }
        isPaused = true;
        //Do menu button stuff
    }

    // Update is called once per frame
    void Update () {
        if (!this.combatOcurring || this.isPaused)
            return;

        UserControllable[] uCArr = GameMaster.instance.thePlayer.theParty;

        //update each player's stamina
        for (int i = 0; i < uCArr.Length; i++) {
            if(uCArr[i] != null && uCArr[i].isAlive)
            {
                Resource stamina = uCArr[i].stamina;
                stamina.add((decimal)((float)uCArr[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 10 ));
            }
        }

        //update each monster's stamina
        for(int i = 0; i < monsters.Length;i++)
        {
            if (monsters[i] != null && monsters[i].isAlive)
            {
                Resource stamina = monsters[i].stamina;
                stamina.add((decimal)((float)monsters[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 10));
            }
        }

        for (int i = 0; i < monsters.Length; i++)
        {
            //If the party isn't dead, do the next Monster's AI
            if (GameMaster.instance.thePlayer.partyIsDead == false)
            {
                if (monsters[i] != null)
                {
                    monsters[i].doBattleAI();
                }
            }
        }

    }

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static BattleScript instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first GameMaster object in the scene.
                s_Instance = FindObjectOfType(typeof(BattleScript)) as BattleScript;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("BattleScript");
                s_Instance = obj.AddComponent(typeof(BattleScript)) as BattleScript;
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
