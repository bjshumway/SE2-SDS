using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour {
    public bool combatOcurring;

    public GameObject DemonSkull;

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

    public void beginCombat()
    {
        this.combatOcurring = true;
        GameObject.Find("UserControllable 1 StaminaBar").GetComponent<RectTransform>().sizeDelta = new Vector2(0,14);

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
    }


    // Update is called once per frame
    void Update () {
        if (!this.combatOcurring)
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
                    stamina.value = stamina.value + uCArr[i].stats["dexterity"].effectiveLevel * Time.smoothDeltaTime * 3.5;
                    Debug.Log(stamina.value);
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
