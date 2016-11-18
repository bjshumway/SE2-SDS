using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreationMenu : MonoBehaviour {

    private static int currHeadNum;
    private static int currHeadColorNum;
    private static UserControllable currentUC;
    private static bool fighterSelected;
    private static bool mageSelected;
    private static bool rogueSelected;

    public GameObject newCharacterHasJoinedPopup;
    public GameObject alreadySelectedClassPopup;

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static CharacterCreationMenu s_Instance = null;


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
    public static void load(UserControllable uC, bool isFirstChar)
    {
        GameObject.Find("NamePlayerInput").GetComponent<InputField>().text = uC.name;
        currentUC = uC;
        GameMaster.instance.switchCamera(1);

        instance.newCharacterHasJoinedPopup.GetComponentInChildren<Text>().text = MLH.tr("A new character has joined!");

        if (!isFirstChar)
        {
            instance.newCharacterHasJoinedPopup.SetActive(true);
        }

        //Randomize the character's initial look
        System.Random r = new System.Random();
        int headCycles = r.Next(0, 2);
        int headColorCycles = r.Next(0, 3);


        for (int i = 0; i < headCycles; i++)
        {
            instance.cycleBodyPart2("head right");
        }
        for (int i = 0; i < headColorCycles; i++)
        {
            instance.cycleBodyPartColor("head right");
        }


    }


    //Cycles a body part, e.g. head, eyes, hair
    //Forget about the "2" at the end of this name, it's for backwards compatibility.
    public void cycleBodyPart2(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log("Inside cycleBodyPart");
        if (bodyPartAndDir[0] == "head")
        {
            if (bodyPartAndDir[1] == "right")
            {
                currHeadNum++;
            }
            else
            {
                currHeadNum--;
            }

            if (currHeadNum < 0)
            {
                currHeadNum = UserControllableLookConfig.instance.heads.Length - 1;
            }
            else if (currHeadNum > UserControllableLookConfig.instance.heads.Length - 1)
            {
                currHeadNum = 0;
            }

            GameObject.Find("CharCreationHead").GetComponent<Image>().sprite = UserControllableLookConfig.instance.heads[currHeadNum];
        }
    }

    //Cycles the color of a particular body part
    public void cycleBodyPartColor(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log(UserControllableLookConfig.instance.colors.Length);
        if (bodyPartAndDir[0] == "head")
        {

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
                currHeadColorNum = UserControllableLookConfig.instance.colors.Length / 4 - 1;
            }
            else if (currHeadColorNum > UserControllableLookConfig.instance.colors.Length / 4 - 1)
            {
                currHeadColorNum = 0;
            }

            Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                     (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                     (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                     (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);

            GameObject.Find("CharCreationHead").GetComponent<Image>().color = newColor;
        }
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
           (alreadySelectedClassPopup.GetComponent<DisableAfterShortWhile>()).showParentTemporarily(3);
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
                //Default weapon
                uC.equipWeapon(new MeleeWeapon("Rusty Sword", 1, false, 1, 1, Weapon.weaponClass.Melee,Weapon.weaponType.balanced, "You found this sword on a long-forgotten battlefield."));
                GameMaster.instance.thePlayer.inventory.addItem(Gen.meleeWeapon(3));

                //Populate some melee weapons in the shop
                ShopInventoryScript.instance.theShop.addItem(Gen.meleeWeapon(5));
                ShopInventoryScript.instance.theShop.addItem(Gen.meleeWeapon(10));
                ShopInventoryScript.instance.theShop.addItem(Gen.meleeWeapon(15));

                break;
            case 2:
                uC.classType = UserControllable.classTypes.mage;
                mageSelected = true;
                uC.learnAbility(Ability.mageAbilities[0]); //Learn Arcane Destruction by Default
                //Default mage weapon
                uC.equipWeapon(new MagicWeapon("Flimsy Wand", 1, false, 1, 1, Weapon.weaponClass.Magic, Weapon.weaponType.balanced, "Could snap any day now."));

                //Populate some mage weapons in the shop
                ShopInventoryScript.instance.theShop.addItem(Gen.magicWeapon(5));
                ShopInventoryScript.instance.theShop.addItem(Gen.magicWeapon(10));
                ShopInventoryScript.instance.theShop.addItem(Gen.magicWeapon(15));

                break;
            case 3:
                uC.classType = UserControllable.classTypes.rogue; //Learn Bow Attack by default
                rogueSelected = true;
                uC.learnAbility(Ability.rogueAbilities[0]); //Learn Bow Attack by default
                //Default rogue weapon
                uC.equipWeapon(new RangedWeapon("Weak Bow", 1, false, 1, 1, Weapon.weaponClass.Ranged, Weapon.weaponType.balanced, "Gets the job done."));

                //Populate some rogue weapons in the shop
                ShopInventoryScript.instance.theShop.addItem(Gen.rangedWeapon(5));
                ShopInventoryScript.instance.theShop.addItem(Gen.rangedWeapon(10));
                ShopInventoryScript.instance.theShop.addItem(Gen.rangedWeapon(15));

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
    
    //Handles clicking of "New Character has joined!"
    public void disappearNewCharacterHasJoinedPopup()
    {
        instance.newCharacterHasJoinedPopup.SetActive(false);
    }

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static CharacterCreationMenu instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first GameMaster object in the scene.
                s_Instance = FindObjectOfType(typeof(CharacterCreationMenu)) as CharacterCreationMenu;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GameMaster");
                s_Instance = obj.AddComponent(typeof(CharacterCreationMenu)) as CharacterCreationMenu;
                Debug.Log("Could not locate an GameMaster object. GameMaster was Generated Automaticly.");
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
