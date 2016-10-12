using UnityEngine;
using System.Collections;

public class SkillSelectionScript : MonoBehaviour {

    public static int statTotal = 10;
    public static int strength = 0;
    public static int intellect = 0;
    public static int dexterity = 0;
    public static int cunning = 0;
    public static int charisma = 0;

    // Use this for initialization
    void Start () {
	
	}

    public void goToNextScene()
    {
        Debug.Log("go to next scene in skillselection script");
        GameMaster.instance.thePlayer.setStatLevels(new int[] { charisma, cunning, dexterity, intellect, strength });
        GameMaster.instance.switchCamera(4);
    }

    public void StrDec()
    {
       if (statTotal < 10 && strength > 0)
        {
            statTotal++;
            strength--;
            Debug.Log("statTotal:" + statTotal + "strength" + strength);
        }
    }

    public void IntDec()
    {
        if (statTotal < 10 && intellect > 0)
        {
            statTotal++;
            intellect--;
        }
    }

    public void DexDec()
    {
        if (statTotal < 10 && dexterity > 0)
        {
            statTotal++;
            dexterity--;
        }
    }

    public void CunDec()
    {
        if (statTotal < 10 && cunning > 0)
        {
            statTotal++;
            cunning--;
        }
    }

    public void CharDec()
    {
        if (statTotal < 10 && charisma > 0)
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
