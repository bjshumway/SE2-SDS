using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillSelectionScript : MonoBehaviour {

    public static int statTotal = 10;
    public static int strength = 0;
    public static int intellect = 0;
    public static int dexterity = 0;
    public static int cunning = 0;
    public static int charisma = 0;




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


        cunning = uC.stats["cunning"].level;
        dexterity = uC.stats["dexterity"].level;
        charisma = uC.stats["charisma"].level;
        strength = uC.stats["strength"].level;
        intellect = uC.stats["intellect"].level;


        GameMaster.instance.switchCamera(3);
    }

    public void goToNextScene()
    {
        Debug.Log("go to next scene in skillselection script");
        currentUC.setStatLevels(new int[] { charisma, cunning, dexterity, intellect, strength });
        GameMaster.instance.switchCamera(4);
    }

    public void StrDec()
    {
       if (statTotal < 10 &&  strength > currentUC.stats["strength"].level)
        {
            statTotal++;
            strength--;
            Debug.Log("statTotal:" + statTotal + "strength" + strength);
        }
    }

    public void IntDec()
    {
        if (statTotal < 10 && intellect > currentUC.stats["intellect"].level)
        {
            statTotal++;
            intellect--;
        }
    }

    public void DexDec()
    {
        if (statTotal < 10 && dexterity > currentUC.stats["dexterity"].level)
        {
            statTotal++;
            dexterity--;
        }
    }

    public void CunDec()
    {
        if (statTotal < 10 && cunning > currentUC.stats["cunning"].level)
        {
            statTotal++;
            cunning--;
        }
    }

    public void CharDec()
    {
        if (statTotal < 10 && charisma > currentUC.stats["charisma"].level)
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
}
