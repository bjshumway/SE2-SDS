using UnityEngine;
using System.Collections;

public class Monster : Actor
{

    public static string[] monsterTypesByLevel(int level)
    {
        switch(level)
        {
            case 1:
                return new string[] { "spider" };
            case 2:
                return new string[] { "lich" };
            case 3:
                return new string[] { "dragon" };
            case 4:
                return new string[] { "god" };
        }

        System.Console.WriteLine("level out of bounds for function monsterTypesByLevel");
        return null;
    }

    public Monster(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null)
: base(name, level, title, resources, statArray)
    {
        return;
    }



}
