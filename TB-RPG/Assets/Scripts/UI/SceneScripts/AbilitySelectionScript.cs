using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilitySelectionScript : MonoBehaviour
{
    public int remainingPoints;
    
    public UserControllable currentUC;
    public GameObject headImage;
    public GameObject nameOfUc;
    public GameObject abilityLearnHelp;

    public GameObject NewAbToSpend;


    public Ability selectedAbility;

    private static AbilitySelectionScript s_Instance = null;



    // Use this for initialization
    void Start()
    {
        headImage = GameObject.Find("HeadAbSelect");
        nameOfUc = GameObject.Find("NameAbSelect");
        abilityLearnHelp = GameObject.Find("AbilityLearnHelp");

        initAbilityPositionsAndClickEvents();
        NewAbToSpend = Resources.Load("AbilityRelated/NewAbToSpend") as GameObject;
        NewAbToSpend = (GameObject) Instantiate(NewAbToSpend, NewAbToSpend.transform.position, NewAbToSpend.transform.rotation);
        NewAbToSpend.transform.SetParent(GameObject.Find("AbilitySelectCanvas").transform, false);
        NewAbToSpend.GetComponent<Button>().onClick.AddListener(delegate { AbilitySelectionScript.instance.clickNewAbilityPointEarned(); });
        NewAbToSpend.SetActive(false);
        selectedAbility = null;
    }

    public void clickedAbility(Ability ab)
    {
        if(ab.isLearned || remainingPoints == 0)
        {
            //ignore the click
            return;
        }

        //Still here? Bring up the AbilityApprovalPopup and set its variables.
        selectedAbility = ab;
        showApprovalPopup();

    }

    //Shows the popup that asks whether the user wants to learn the ability
    public void showApprovalPopup()
    {
        GameObject cont = GameObject.Find("AbilityApprovalQuestion");
        if (cont == null)
        {
            cont = Resources.Load("AbilityRelated/AbilityApprovalPopupContainer") as GameObject;
            cont = GameObject.Instantiate(cont, cont.transform.position, cont.transform.rotation) as GameObject;
            cont.transform.SetParent(GameObject.Find("AbilitySelectCanvas").transform, false);
            GameObject.Find("YesApproveAbility").GetComponent<Button>().onClick.AddListener(delegate { AbilitySelectionScript.instance.acceptLearningAbility();  });
            GameObject.Find("NoApproveAbility").GetComponent<Button>().onClick.AddListener(delegate { AbilitySelectionScript.instance.declineLearningAbility(); });

        }

        cont.SetActive(true);

        GameObject.Find("AbilityApprovalQuestion").GetComponent<Text>().text = MLH.tr("LEARN ") + MLH.tr(selectedAbility.name) + "?";
        //GameObject.Find("AbilityApprovalDescription").GetComponent<Text>().text = MLH.tr(selectedAbility.toolTip);
    }

    public void removeApprovalPopup()
    {
        GameObject obj = GameObject.Find("AbilityApprovalPopupContainer(Clone)");
        obj.SetActive(false);
    }

    public void acceptLearningAbility()
    {
        currentUC.learnAbility(selectedAbility);
        selectedAbility = null;
        remainingPoints--;
        currentUC.remainingAbilityPoints--;
        GameObject.Find("AbilityMenuRemainingPoints").GetComponent<Text>().text = remainingPoints.ToString();
        AudioControl.playSound("selection");
        removeApprovalPopup();
    }

    public void declineLearningAbility()
    {
        selectedAbility = null;
        removeApprovalPopup();
    }

    //Loop through all abilities, and set their x,y positions 
    public void initAbilityPositionsAndClickEvents()
    {
        int xStart = -330;
        int xPos = xStart;
        int yPos = 184;
        int xDelta = 183;
        int yDelta = -156;
        int xMax = 512;


        for (int i = 0; i < Ability.fighterAbilities.Length ||
              i < Ability.mageAbilities.Length             ||
              i < Ability.rogueAbilities.Length; i++)
        {
            if(i < Ability.fighterAbilities.Length)
            {
                Ability ab = Ability.fighterAbilities[i];
                ab.xPosSelectionMenu = xPos;
                ab.yPosSelectionMenu = yPos;

                ab.learnButton.GetComponent<Button>().onClick.AddListener(
                        delegate { AbilitySelectionScript.instance.clickedAbility(ab); } );
            }

            if (i < Ability.mageAbilities.Length)
            {
                Ability ab = Ability.mageAbilities[i];
                ab.xPosSelectionMenu = xPos;
                ab.yPosSelectionMenu = yPos;

                ab.learnButton.GetComponent<Button>().onClick.AddListener(
                        delegate { AbilitySelectionScript.instance.clickedAbility(ab); });


            }

            if (i < Ability.rogueAbilities.Length)
            {
                Ability ab = Ability.rogueAbilities[i];
                ab.xPosSelectionMenu = xPos;
                ab.yPosSelectionMenu = yPos;

                ab.learnButton.GetComponent<Button>().onClick.AddListener(
                        delegate { AbilitySelectionScript.instance.clickedAbility(ab); });

            }

            xPos += xDelta;
            if(xPos > xMax)
            {
                yPos += yDelta;
                xPos = xStart;
            }

        }
    }
    

    //Switches the camera to this scene
    //Populates the Image and Name on the canvas so that we know which uC is here
    //Todo: the abilities to choose from should be based on the uC's class, and what the uC has already chosen.
    public void load(UserControllable uC)
    {
        currentUC = uC;
        remainingPoints = uC.remainingAbilityPoints;
        headImage.GetComponent<Image>().sprite = uC.headType;
        headImage.GetComponent<Image>().color = uC.headColor;
        nameOfUc.GetComponent<Text>().text = uC.name;

        if(uC.mustBeToldOfNewAbilityPointToSpend)
        {

            uC.mustBeToldOfNewAbilityPointToSpend = false;
            showNewAbilityPointEarned();
        }

        GameMaster.instance.switchCamera(2);
        GameObject.Find("AbilityMenuRemainingPoints").GetComponent<Text>().text = remainingPoints.ToString();


        populateAbilities(currentUC);

    }

    public void showNewAbilityPointEarned()
    {
        AudioControl.playSound("level_up");
        NewAbToSpend.SetActive(true);
    }

    public void clickNewAbilityPointEarned()
    {
        NewAbToSpend.SetActive(false);
    }

    public void goToNextScene()
    {
        //Clear the tooltip area
        GameObject.Find("AbToolTipArea").GetComponent<Text>().text = "";
        SkillSelectionScript.load(currentUC);
    }

    //Populates the abilities based on currentUC's class
    public void populateAbilities(UserControllable currentUc)
    {
        Ability[] abs = null;

        if(currentUc.classType == UserControllable.classTypes.fighter)
        {
            abs = Ability.fighterAbilities;
        }
        else if(currentUc.classType == UserControllable.classTypes.mage)
        {
            abs = Ability.mageAbilities;
        }
        else
        {
            abs = Ability.rogueAbilities;
        }

        //Deactive all abilities that are not a part of this class
        for(int i = 0; i < Ability.fighterAbilities.Length && abs != Ability.fighterAbilities; i++)
        {
            Ability.fighterAbilities[i].learnButton.SetActive(false);
        }

        for (int i = 0; i < Ability.mageAbilities.Length && abs != Ability.mageAbilities; i++)
        {
            Ability.mageAbilities[i].learnButton.SetActive(false);
        }

        for (int i = 0; i < Ability.rogueAbilities.Length && abs != Ability.rogueAbilities; i++)
        {
            Ability.rogueAbilities[i].learnButton.SetActive(false);
        }



        //Now place the abs, and activate them
        for (int i = 0; i < abs.Length; i++)
        {
            Ability ab = abs[i];

            int xPos = ab.xPosSelectionMenu;
            int yPos = ab.yPosSelectionMenu;
            abs[i].learnButton.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            abs[i].learnButton.SetActive(true);

        }

        
        //abs = Ability.abilitiesByClass[currentUc.classType];

    }


    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static AbilitySelectionScript instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AbilitySelectionScript object in the scene.
                s_Instance = FindObjectOfType(typeof(AbilitySelectionScript)) as AbilitySelectionScript;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("AbilitySelectionScript");
                s_Instance = obj.AddComponent(typeof(AbilitySelectionScript)) as AbilitySelectionScript;
                Debug.Log("Could not locate an AbilitySelectionScript object. AbilitySelectionScript was Generated Automaticly.");
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
