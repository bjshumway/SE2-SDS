using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreationMenu : MonoBehaviour {

    public static int currHeadNum;
    public static int currHeadColorNum;
    public static UserControllable currentUC;
    private static bool fighterSelected;
    private static bool mageSelected;
    private static bool rogueSelected;

    public void Start()
    {
        //Debug.Log("Inside start of CharacterCreationMenu");
        //GameObject.FindWithTag("head").GetComponent<Image>().sprite = Heads[0];

        fighterSelected = false;
        mageSelected = false;
        rogueSelected = false;

        currHeadColorNum = 0;
        currHeadNum = 0;
        GameObject.FindWithTag("head").GetComponent<Image>().color = new Color32((byte)UserControllableLookConfig.instance.colors[0,0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,3]);

    }


    //Switches the camera to this scene
    //Populates the Image and Name on the canvas so that we know which uC is here
    public static void load(UserControllable uC)
    {
        currentUC = uC;
        GameMaster.instance.switchCamera(1);
    }


    //Cycles a body part, e.g. head, eyes, hair
    public void cycleBodyPart2(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log("Inside cycleBodyPart");

        if(bodyPartAndDir[1] == "right")
        {
            currHeadNum++;
        }
        else
        {
            currHeadNum--;
        }

        if(currHeadNum < 0)
        {
            currHeadNum = UserControllableLookConfig.instance.heads.Length - 1;
        } else if(currHeadNum > UserControllableLookConfig.instance.heads.Length -1)
        {
            currHeadNum = 0;
        }

        GameObject.Find("CharCreationHead").GetComponent<Image>().sprite = UserControllableLookConfig.instance.heads[currHeadNum];
    }

    //Cycles the color of a particular body part
    public void cycleBodyPartColor(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log(UserControllableLookConfig.instance.colors.Length);
        if (bodyPartAndDir[1] == "right")
        {
            currHeadColorNum++;
        }
        else
        {
            currHeadColorNum--;
        }

        if (currHeadColorNum < 0)
        {
            currHeadColorNum = UserControllableLookConfig.instance.colors.Length/4 - 1;
        }
        else if (currHeadColorNum > UserControllableLookConfig.instance.colors.Length/4 - 1)
        {
            currHeadColorNum = 0;
        }

        Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);

        GameObject.Find("CharCreationHead").GetComponent<Image>().color = newColor;
    }

    public void goToNextScene()
    {
        Dropdown classSelect = GameObject.Find("ClassSelect").GetComponent<Dropdown>();
        if (classSelect.value == 0)
        {
            //The user hasn't yet selected a class
            //Todo: put up a message saying you must select a class first
            return;
        } else if (fighterSelected == true && classSelect.value == 1 ||
                   mageSelected == true && classSelect.value  == 2  ||
                   rogueSelected == true && classSelect.value == 3){
            //The user selected a class that's already been selected.
            //Todo: put up a message saying you must select a class first
            return;
        }


        //Still here? Go ahead and set the userControllable's class, and look
        UserControllable uC = currentUC;

        switch(classSelect.value)
        {
            case 1:
                uC.classType = UserControllable.classTypes.fighter;
                fighterSelected = true;
                uC.learnAbility(Ability.fighterAbilities[0]); //Learn Attack by Default
                break;
            case 2:
                uC.classType = UserControllable.classTypes.mage;
                mageSelected = true;
                uC.learnAbility(Ability.mageAbilities[0]); //Learn Arcane Destruction by Default
                break;
            case 3:
                uC.classType = UserControllable.classTypes.rogue; //Learn Bow Attack by default
                rogueSelected = true;
                uC.learnAbility(Ability.rogueAbilities[0]); //Learn Bow Attack by default
                break;
        }

        Sprite newHead = UserControllableLookConfig.instance.heads[currHeadNum];
        uC.headType = newHead;
        Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);
        uC.headColor = newColor;

        uC.battleHead.sprite = uC.headType;
        uC.battleHead.color = uC.headColor;
        uC.battleHead.enabled = true;
        uC.battleHealthBar.enabled = true;
        uC.battleStaminaBar.enabled = true;

        uC.name = GameObject.Find("NamePlayerInput").GetComponent<InputField>().text;

        GameObject.Find("Battle UC " + uC.id + " HealthBar").SetActive(true);
        GameObject.Find("Battle UC " + uC.id + " StaminaBar").SetActive(true);



        //Pass in a reference to the current character, so that it knows which character to load
        AbilitySelectionScript.load(uC);


    }

}
