using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilitySelectionScript : MonoBehaviour
{
    public static int remainingPoints;
    
    public static UserControllable currentUC;
    public static GameObject headImage;
    public static GameObject nameOfUc;

    public static Ability selectedAbility;

    
    // Use this for initialization
    void Start()
    {
        headImage = GameObject.Find("HeadAbSelect");
        nameOfUc = GameObject.Find("NameAbSelect");

        initAbilityPositionsAndClickEvents();

        selectedAbility = null;
    }

    public static void clickedAbility(Ability ab)
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
    public static void showApprovalPopup()
    {
        GameObject cont = GameObject.Find("AbilityApprovalQuestion");
        if (cont == null)
        {
            cont = Resources.Load("AbilityRelated/AbilityApprovalPopupContainer") as GameObject;
            cont = GameObject.Instantiate(cont, cont.transform.position, cont.transform.rotation) as GameObject;
            cont.transform.SetParent(GameObject.Find("AbilitySelectCanvas").transform, false);
            GameObject.Find("YesApproveAbility").GetComponent<Button>().onClick.AddListener(delegate { AbilitySelectionScript.acceptLearningAbility();  });
            GameObject.Find("NoApproveAbility").GetComponent<Button>().onClick.AddListener(delegate { AbilitySelectionScript.declineLearningAbility(); });

        }

        cont.SetActive(true);

        GameObject.Find("AbilityApprovalQuestion").GetComponent<Text>().text = "LEARN " + selectedAbility.name + "?";
        GameObject.Find("AbilityApprovalDescription").GetComponent<Text>().text = selectedAbility.toolTip;
    }

    public static void removeApprovalPopup()
    {
        GameObject obj = GameObject.Find("AbilityApprovalPopupContainer(Clone)");
        obj.SetActive(false);
    }

    public static void acceptLearningAbility()
    {
        currentUC.learnAbility(selectedAbility);
        selectedAbility = null;
        remainingPoints--;
        GameObject.Find("AbilityMenuRemainingPoints").GetComponent<Text>().text = remainingPoints.ToString();
        removeApprovalPopup();
    }

    public static void declineLearningAbility()
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
                        delegate { AbilitySelectionScript.clickedAbility(ab); } );
            }

            if (i < Ability.mageAbilities.Length)
            {
                Ability ab = Ability.mageAbilities[i];
                ab.xPosSelectionMenu = xPos;
                ab.yPosSelectionMenu = yPos;

                ab.learnButton.GetComponent<Button>().onClick.AddListener(
                        delegate { AbilitySelectionScript.clickedAbility(ab); });


            }

            if (i < Ability.rogueAbilities.Length)
            {
                Ability ab = Ability.rogueAbilities[i];
                ab.xPosSelectionMenu = xPos;
                ab.yPosSelectionMenu = yPos;

                ab.learnButton.GetComponent<Button>().onClick.AddListener(
                        delegate { AbilitySelectionScript.clickedAbility(ab); });

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
    public static void load(UserControllable uC)
    {
        currentUC = uC;
        remainingPoints = uC.remainingResourcePoints;
        headImage.GetComponent<Image>().sprite = uC.headType;
        headImage.GetComponent<Image>().color = uC.headColor;
        nameOfUc.GetComponent<Text>().text = uC.name;

        GameMaster.instance.switchCamera(2);

        remainingPoints = uC.remainingResourcePoints;

        GameObject.Find("AbilityMenuRemainingPoints").GetComponent<Text>().text = remainingPoints.ToString();


        populateAbilities(currentUC);

    }

    public void goToNextScene()
    {
        SkillSelectionScript.load(currentUC);
    }

    //Populates the abilities based on currentUC's class
    public static void populateAbilities(UserControllable currentUc)
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
        for(int i = 0; i < Ability.mageAbilities.Length && abs != Ability.mageAbilities; i++)
        {
            Ability.mageAbilities[i].learnButton.SetActive(false);
        }

        for (int i = 0; i < Ability.mageAbilities.Length && abs != Ability.mageAbilities; i++)
        {
            Ability.mageAbilities[i].learnButton.SetActive(false);
        }

        for (int i = 0; i < Ability.mageAbilities.Length && abs != Ability.mageAbilities; i++)
        {
            Ability.mageAbilities[i].learnButton.SetActive(false);
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



}
