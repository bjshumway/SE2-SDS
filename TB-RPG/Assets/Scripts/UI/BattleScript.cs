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
        Debug.Log("inside combat start");


        GameObject.Find("UserControllable 2 HeadType").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        GameObject.Find("UserControllable 2 HealthBar").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        GameObject.Find("UserControllable 2 StaminaBar").GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        GameObject.Find("UserControllable 3 HeadType").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        GameObject.Find("UserControllable 3 HealthBar").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        GameObject.Find("UserControllable 3 StaminaBar").GetComponent<Image>().color = new Color32(255, 255, 255, 0);

    }

    //Refreshes the stats displayed for an actor,
    //Possible values for r are: health,stamina,all
    //If actor is null, then refresh it for all actors
    public void refreshStatDisplay(Actor a = null, string r = "all")
    {
        if(a != null)
        {
            if(r == "health" || r == "all")
            {
                RectTransform rt;
                if (a.isUserControllable)
                {
                    //replace this with changing the slider
                    GameObject.Find("UserControllable " + a.id + " HealthBar").GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                } else
                {
                    //replace this with changing the slider
                    GameObject.Find("Monster " + a.id + " HealthBar").GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                    
                    //.GetComponent<Image>().rectTransform.Translate(new Vector3(1, 1, 1));
                    //= new Color32(0, 0, 255, 255);

                    //rt = GameObject.Find("asdf").GetComponent<RectTransform>();
                }

                //rt.sizeDelta = new Vector2((float).1, (float).1);
            }

            if(r == "stamina" || r == "all")
            {
                //Todo: implement update stamina
            }
        }
        else //a == null, so update everyone
        {
            foreach (UserControllable uC in GameMaster.instance.thePlayer.theParty)
            {
                if (r == "health" || r == "all")
                {
                    //change slider here
                }

                if (r == "stamina" || r == "all")
                {
                    //change slider here
                }
            }
            foreach (Monster m in monsters)
            {
                if (r == "health" || r == "all")
                {
                    GameObject.Find("demonSkull").GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                }

                if (r == "stamina" || r == "all")
                {
                    //Update stamina
                }
            }
        }
    }

    //Refreshes the user controllable sprites
    public void refreshUCSprites()
    {
        UserControllable[] theParty = GameMaster.instance.thePlayer.theParty;
        Debug.Log(theParty.ToString());



        for (int i = 0; i < theParty.Length; i++)
        {
            if (theParty[i] == null)
                break;

            //Make any UC that isn't on the screen, gets drawn on the screen
            if (!theParty[i].imageIsInBattleScreen)
            {
                theParty[i].imageIsInBattleScreen = true;
                GameObject.Find("UserControllable " + (i+1) + " HeadType").GetComponent<Image>().sprite = theParty[i].headType;
                GameObject.Find("UserControllable " + (i+1) + " HeadType").GetComponent<Image>().color = theParty[i].headColor;
            }
        }
    }


    //Todo: update this function so that it takes an array of monsters as an argument
    public void beginCombat(Monster[] monsters)
    {
        this.combatOcurring = true;
        this.monsters = monsters;

        //place each monster's GUI related stuff onto the battle field

        //Set all players and monster's stamina's to a random amount
        //then call refreshStatDisplay
        GameObject.Find("UserControllable 1 StaminaBar").GetComponent<RectTransform>().sizeDelta = new Vector2(0,14);


        //Loop through the characters and set their health and stamina bars
        //Set stamina to a random initial state
        UserControllable[] theParty = GameMaster.instance.thePlayer.theParty;
        for (int i = 0; i < theParty.Length; i++)
        {
            if(theParty[i] == null)
                break;

            //Make any UC that isn't on the screen, gets drawn on the screen
            if (!theParty[i].imageIsInBattleScreen)
            {
                theParty[i].imageIsInBattleScreen = true;
                GameObject.Find("UserControllable " + (i + 1)+ " HeadType").GetComponent<Image>().sprite = theParty[i].headType;
                GameObject.Find("UserControllable " + (i + 1)+ " HeadType").GetComponent<Image>().color = theParty[i].headColor;
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
        if(ab != null)
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
            if(uCArr[i] != null)
            {
                Resource stamina = uCArr[i].stamina;

                ///Debug.Log(stamina.maxValue);
                  if (stamina.value < stamina.maxValue)
                  {
                    stamina.add((decimal)((float)uCArr[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 3.5));
                    //Debug.Log(stamina.value);
                    //Todo: use a slider instead of an image
                    RectTransform rt = GameObject.Find("UserControllable 1 StaminaBar").GetComponent<RectTransform>();
                    rt.sizeDelta =  rt.sizeDelta + new Vector2((float)stamina.value / (float)3.5, 0);
                    //Time.deltaTime * uCArr[i].stats["dexterity"].effectiveLevel * 10;
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
                Debug.Log("Could not locate an BattleScript object. BattleScript was Generated Automaticly.");
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
