using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillSelectionScript : MonoBehaviour {

    public static int statTotal = 0;
    public static int statMax = 0;
    public static int strength = 0;
    public static int intellect = 0;
    public static int dexterity = 0;
    public static int cunning = 0;
    public static int charisma = 0;

    public static int resourceTotal = 0;
    public static int resourceMax = 0;
    public static int stamina = 0;
    public static int health = 0;




    public static UserControllable currentUC;
    public static GameObject headImage;
    public static GameObject nameOfUc;

    // Use this for initialization
    void Start () {
        headImage = GameObject.Find("HeadSkillSelect");
        nameOfUc = GameObject.Find("NameStatSelect");
	}

    //Switches the camera to this scene
    //Populates the Image and Name on the canvas so that we know which uC is here
    public static void load(UserControllable uC)
    {
        currentUC = uC;
        headImage.GetComponent<Image>().sprite = uC.headType;
        headImage.GetComponent<Image>().color = uC.headColor;
        nameOfUc.GetComponent<Text>().text = uC.name;

        statTotal = uC.remainingStatPoints;
        statMax = uC.remainingStatPoints;

        cunning = uC.stats["cunning"].level;
        dexterity = uC.stats["dexterity"].level;
        charisma = uC.stats["charisma"].level;
        strength = uC.stats["strength"].level;
        intellect = uC.stats["intellect"].level;

        resourceTotal = uC.remainingResourcePoints;
        resourceMax = uC.remainingResourcePoints;
        
        health = (int)uC.health.maxValue;
        stamina = (int)uC.stamina.refreshSpeed;

        //TODO: If this is our first time here, the lower button reads "Start The Adventure" 
        //      If there is another player to level up after this, it should read  "Level up <name>!"
        //      Otherwise, it should read "Back to The Adventure!"

        GameMaster.instance.switchCamera(3);
    }

    public void goToNextScene()
    {
        //Debug.Log("go to next scene in skillselection script");
        currentUC.setStatLevels(new int[] { charisma, cunning, dexterity, intellect, strength });
        currentUC.health.maxValue = health;
        currentUC.stamina.refreshSpeed = stamina;
        if (VictoryHandler.instance.state == VictoryHandler.vhState.levelingUCs ||
            VictoryHandler.instance.state == VictoryHandler.vhState.addingPartyMember)
        {
            UserControllable uC = VictoryHandler.instance.getNextUCToLevel();
            if (uC == null)
            {
                //Go to overworld
                GameMaster.instance.switchCamera(4);
            }
            else
            {
                AbilitySelectionScript.load(uC);
            }
        }
        else
        {
            //Go to overworld
            GameMaster.instance.switchCamera(4);
        }

    }

    public void HealthDec()
    {
        if (resourceTotal < resourceMax && health > currentUC.health.maxValue)
        {
            resourceTotal++;
            health--;
            //Debug.Log("statTotal:" + statTotal + "strength" + strength);
            Debug.Log("healthDecrease");
        }

    }
    public void StaminaDec()
    {
        if (resourceTotal < resourceMax && stamina > currentUC.stamina.refreshSpeed)
        {
            resourceTotal++;
            stamina--;
            //Debug.Log("statTotal:" + statTotal + "strength" + strength);
            Debug.Log("staminaDecrease");
        }
    }

    public void StrDec()
    {
       if (statTotal < statMax &&  strength > currentUC.stats["strength"].level)
        {
            statTotal++;
            strength--;
            //Debug.Log("statTotal:" + statTotal + "strength" + strength);
        }
    }

    public void IntDec()
    {
        if (statTotal < statMax && intellect > currentUC.stats["intellect"].level)
        {
            statTotal++;
            intellect--;
        }
    }

    public void DexDec()
    {
        if (statTotal < statMax && dexterity > currentUC.stats["dexterity"].level)
        {
            statTotal++;
            dexterity--;
        }
    }

    public void CunDec()
    {
        if (statTotal < statMax && cunning > currentUC.stats["cunning"].level)
        {
            statTotal++;
            cunning--;
        }
    }

    public void CharDec()
    {
        if (statTotal < statMax && charisma > currentUC.stats["charisma"].level)
        {
            statTotal++;
            charisma--;
        }
    }

    public void StrInc()
    {
        if (statTotal > 0)
        {
            statTotal--;
            strength++;
        }
    }

    public void IntInc()
    {
        if (statTotal > 0)
        {
            statTotal--;
            intellect++;
        }
    }

    public void DexInc()
    {
        if (statTotal > 0)
        {
            statTotal--;
            dexterity++;
        }
    }

    public void CunInc()
    {
        if (statTotal > 0)
        {
            statTotal--;
            cunning++;
        }
    }

    public void CharInc()
    {
        if (statTotal > 0)
        {
            statTotal--;
            charisma++;
        }
    }


    public void HealthInc()
    {
        if (resourceTotal > 0)
        {
            resourceTotal--;
            health++;
        }
    }

    public void StaminaInc()
    {
        if (resourceTotal > 0)
        {
            resourceTotal--;
            stamina++;
        }
    }


}
