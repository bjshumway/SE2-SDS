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


    public GameObject ActiveUCFrame;


    public GameObject victoryText;
    public GameObject VictoryPanel;

    public GameObject[] abilityButtons;

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
        GameObject.Find("Battle UC 2 HealthBar").SetActive(false);
        GameObject.Find("Battle UC 2 StaminaBar").SetActive(false);
        GameObject.Find("Battle UC 2 BattleDamageText").SetActive(false);
        GameObject.Find("Battle UC 2 StatusEffectText").SetActive(false);


        GameObject.Find("Battle UC 3 HeadType").GetComponent<Image>().enabled = false;
        GameObject.Find("Battle UC 3 HealthBar").SetActive(false);
        GameObject.Find("Battle UC 3 StaminaBar").SetActive(false);
        GameObject.Find("Battle UC 3 BattleDamageText").SetActive(false);
        GameObject.Find("Battle UC 3 StatusEffectText").SetActive(false);

    }






    //Todo: update this function so that it takes an array of monsters as an argument
    public void beginCombat(Monster[] monsters)
    {
        this.combatOcurring = true;
        this.monsters = monsters;

        //Make the monsters be located correctly on the battlefield
        switch(monsters.Length)
        {
            case 1:
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(-1, 180, 0);
                break;
            case 2:
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(-128, 180, 0);
                monsters[1].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(168, 180, 100);
                break;
            case 3:
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(-330, 180, 0);
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(0, 180, 0);
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(330, 180, 0);
                break;
            case 4:
                monsters[0].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(-345, 180, 0);
                monsters[1].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(-113, 180, 0);
                monsters[2].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(113, 180, 0);
                monsters[3].monsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(345, 180, 0);
                break;
        }

        //Testing stats
        //GameMaster.instance.thePlayer.stats["cunning"].setLevel(100);

        //Party members stamina starts at full
        UserControllable[] theParty = GameMaster.instance.thePlayer.theParty;
        for (int i = 0; i < theParty.Length; i++)
        {
            if(theParty[i] != null)
            {
                Resource stamina = theParty[i].stamina;
                //stamina.setValue(random.Next((int)stamina.value));
                //theParty[i].battleStaminaBar.value = (float)stamina.value;
                stamina.setValue(random.Next((int)stamina.maxValue));
            }
        }

        //Set  monster's stamina's to a random amount
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] != null)
            {
                Resource stamina = monsters[i].stamina;
                stamina.setValue(random.Next((int)(stamina.maxValue/2)));
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


        //Nullify the current activeCharacter's currentAbSlt for each ability
        if (activeCharacter != null)
        {
            for (int i = 0; i < activeCharacter.abilities.abilities.Length; i++)
            {
                Ability ab = activeCharacter.abilities.abilities[i];
                if (ab != null)
                {
                    ab.currentAbSlot = null;
                }
            }
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
                ab.currentAbSlot = abilityButtons[i];
                GameObject.Find("AbSlot" + (i + 1) + "_Name").GetComponent<Text>().text = ab.name;
                GameObject.Find("AbSlot" + (i + 1) + "_Cost").GetComponent<Text>().text = "" + ab.stamina;
            } else
            {
                GameObject.Find("AbSlot" + (i + 1) + "_Name").GetComponent<Text>().text = "EmptySlot";
                GameObject.Find("AbSlot" + (i + 1) + "_Cost").GetComponent<Text>().text = "";
            }
        }

        //Place the ActiveUCFrame behind this guy's image
        ActiveUCFrame.transform.localPosition = activeCharacter.battleHead.transform.localPosition;

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
        if(ab != null && ((activeCharacter.stamina.value == 100) || (ab.stamina == 0)))
        {
            ab.cast();
        }
    }

    //Transfers clicking on a mosnter into pipeInputFunc
    public void monsterClick(string arg)
    {
        if (pipeInputFunc != null)
        {
            pipeInputFunc(arg);
            return;
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
    //TODO: simplify update by placing each block of code into it's own function, and call them from update. 
    void Update () {
        if (!this.combatOcurring || this.isPaused)
            return;

        //Handle KeyPresses
        handleKeyPresses();

        //If active usercontrollable doesn't have 100 stamina, foggify the ability button by shading it
        handleFoggifyAbilityButtons();

        updateUserControllablesStamina();

        //update each monster's stamina
        updateMonstersStamina();


        doMonstersAI();

        handleDeadParty();
        handleVictory();
    }

    //Handles keypresses in battle, e.g. ability or switching between uC's
    public void handleKeyPresses()
    {
        if (Input.GetKeyDown("q"))
        {
            GameObject.Find("AbSlot1").GetComponent<Button>().onClick.Invoke();
        } else if(Input.GetKeyDown("w"))
        {
            GameObject.Find("AbSlot2").GetComponent<Button>().onClick.Invoke();
        }
        else if (Input.GetKeyDown("e"))
        {
            GameObject.Find("AbSlot3").GetComponent<Button>().onClick.Invoke();
        }
        else if (Input.GetKeyDown("r"))
        {
            GameObject.Find("AbSlot4").GetComponent<Button>().onClick.Invoke();
        }
        else if (Input.GetKeyDown("t"))
        {
            GameObject.Find("AbSlot5").GetComponent<Button>().onClick.Invoke();
        }
    }

    //If active usercontrollable doesn't have 100 stamina, foggify the ability button by shading it
    //Exception being sword flurry
    public void handleFoggifyAbilityButtons()
    {
        if (activeCharacter.stamina.value != 100)
        {
            for (int i = 1; i < 5; i++)
            {
                GameObject go = GameObject.Find("AbSlot" + i);
                Button b = go.GetComponent<Button>();

                if (GameObject.Find("AbSlot" + i + "_Cost").GetComponent<Text>().text == "0")
                {
                    b.interactable = true;
                    continue;
                }
                b.interactable = false;

            }
        }
        else if (activeCharacter.stamina.value == 100)
        {
            for (int i = 1; i < 5; i++)
            {
                GameObject go = GameObject.Find("AbSlot" + i);
                Button b = go.GetComponent<Button>();
                b.interactable = true;
            }
        }
    }

    public void updateUserControllablesStamina()
    {
        UserControllable[] uCArr = GameMaster.instance.thePlayer.theParty;

        //update each player's stamina
        for (int i = 0; i < uCArr.Length; i++)
        {
            if(uCArr[i] == null) {
                continue;
            }
            if (uCArr[i].isAlive)
            {
                Resource stamina = uCArr[i].stamina;
                stamina.add((decimal)((float)uCArr[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 10));
            }
            if (uCArr[i].stamina.value == 100 && activeCharacter.stamina.value != 100)
            {
                setActiveUC("UserControllable " + (uCArr[i].id - 1));
            }

        }


    }

    public void updateMonstersStamina()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] != null && monsters[i].isAlive)
            {
                Resource stamina = monsters[i].stamina;
                stamina.add((decimal)((float)monsters[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 10));
            }
        }
    }

    public void doMonstersAI()
    {
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

    public void handleDeadParty()
    {
        if (GameMaster.instance.thePlayer.partyIsDead == true)
        {
            this.combatOcurring = false;
            GameMaster.instance.switchCamera(7);
        }
    }

    public void handleVictory()
    {
        bool monstersDied = true;
        foreach (Monster mon in monsters)
        {
            if (mon.isAlive)
            {
                monstersDied = false;
            }
        }
        if (monstersDied)
        {
            //Go to victory function

            this.combatOcurring = false;

            //Delete each monster

            monsters = null;
            Monster.id_increment = 1;

            VictoryPanel.SetActive(true);
            //now set victory text to what happened due to the battle

            //GameMaster.instance.switchCamera(7);
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
