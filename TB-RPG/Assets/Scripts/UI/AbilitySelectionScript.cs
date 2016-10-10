using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilitySelectionScript : MonoBehaviour
{
    public static int count = 0;
    public static string ability1;
    public static string ability2;
    public static string ability3;
    //creates text object for the script
    public Text abl1;
    public Text abl2;
    public Text abl3;

    // Use this for initialization
    void Start()
    {

    }

    public void goToNextScene()
    {
        GameMaster.instance.switchCamera(3);
    }

    public void abil1()
    {
        if (count == 1)
            ability1 = "Ability 1";

        else if (count == 2)
            ability2 = "Ability 1";

        else if (count == 3)
            ability3 = "Ability 1";

    }

    public void abil2()
    {
        if (count == 1)
        {
            ability1 = "Slash";
            playerAbility1.abl1Damage = TestSlash.attack;
            playerAbility1.abl1Accuracy = TestSlash.accuracy;
        }

        else if (count == 2)
        {
            ability2 = "Slash";
            playerAbility2.abl2Damage = TestSlash.attack;
            playerAbility2.abl2Accuracy = TestSlash.accuracy;
        }

        else if (count == 3)
        {
            ability3 = "Slash";
            playerAbility3.abl3Damage = TestSlash.attack;
            playerAbility3.abl3Accuracy = TestSlash.accuracy;
        }

    }

    public void abil3()
    {
        if (count == 1)
        {
            ability1 = "Slam";
            playerAbility1.abl1Damage = TestSlash.attack;
            playerAbility1.abl1Accuracy = TestSlash.accuracy;
        }

        else if (count == 2)
        {
            ability2 = "Slam";
            playerAbility2.abl2Damage = TestSlash.attack;
            playerAbility2.abl2Accuracy = TestSlash.accuracy;
        }

        else if (count == 3)
        {
            ability3 = "Slam";
            playerAbility3.abl3Damage = TestSlash.attack;
            playerAbility3.abl3Accuracy = TestSlash.accuracy;
        }
    }

    public void abil4()
    {
        if (count == 1)
        {
            ability1 = "Attack";
            playerAbility1.abl1Damage = TestSlash.attack;
            playerAbility1.abl1Accuracy = TestSlash.accuracy;
        }

        else if (count == 2)
        {
            ability2 = "Attack";
            playerAbility2.abl2Damage = TestSlash.attack;
            playerAbility2.abl2Accuracy = TestSlash.accuracy;
        }


        else if (count == 3)
        {
            ability3 = "Attack";
            playerAbility3.abl3Damage = TestSlash.attack;
            playerAbility3.abl3Accuracy = TestSlash.accuracy;
        }

    }

    public void abil5()
    {
        if (count == 1)
            ability1 = "Ability 5";
            

        else if (count == 2)
            ability2 = "Ability 5";

        else if (count == 3)
            ability3 = "Ability 5";

    }

    //used to select ability for first slot
    public void ablselect1()
    {
        count = 1;
        
        //highlights number of ability slot player wants to assign 
        abl1.color = Color.yellow;
        abl2.color = Color.white;
        abl3.color = Color.white;
    }

    //used to select ability for second slot
    public void ablselect2()
    {
        count = 2;
        abl2.color = Color.yellow;
        abl1.color = Color.white;
        abl3.color = Color.white;
    }

    // used to select ability for third slot
    public void ablselect3()
    {
        count = 3;
        abl3.color = Color.yellow;
        abl1.color = Color.white;
        abl2.color = Color.white;
    }
}
