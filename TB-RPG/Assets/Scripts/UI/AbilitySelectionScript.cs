using UnityEngine;
using System.Collections;

public class AbilitySelectionScript : MonoBehaviour
{
    public static int count = 0;
    public static string ability1;
    public static string ability2;
    public static string ability3;

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
        if (count == 0)
        {
            ability1 = "Ability 1";
            count++;
        }

        else if (count == 1)
        {
            ability2 = "Ability 1";
            count++;
        }

        else if (count == 2)
        {
            ability3 = "Ability 1";
            count++;
        }

    }

    public void abil2()
    {
        if (count == 0)
        {
            ability1 = "Slash";
            count++;
        }

        else if (count == 1)
        {
            ability2 = "Slash";
            count++;
        }

        else if (count == 2)
        {
            ability3 = "Slash";
            count++;
        }

    }

    public void abil3()
    {
        if (count == 0)
        {
            ability1 = "Slam";
            count++;
        }

        else if (count == 1)
        {
            ability2 = "Slam";
            count++;
        }

        else if (count == 2)
        {
            ability3 = "Slam";
            count++;
        }

    }

    public void abil4()
    {
        if (count == 0)
        {
            ability1 = "Attack";
            count++;
        }

        else if (count == 1)
        {
            ability2 = "Attack";
            count++;
        }

        else if (count == 2)
        {
            ability3 = "Attack";
            count++;
        }

    }

    public void abil5()
    {
        if (count == 0)
        {
            ability1 = "Ability 5";
            count++;
        }

        else if (count == 1)
        {
            ability2 = "Ability 5";
            count++;
        }

        else if (count == 2)
        {
            ability3 = "Ability 5";
            count++;
        }

    }
}
