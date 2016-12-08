using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reaper : Monster
{
    
    
    public Reaper()
        : base("Reaper", //Name of Monster
               "Reaper", //Name of Monster's Prefab
               1,        //level
               2,        //difficulty in level
               0,        //hit accuracy
               true,    //isBoss
               null,     //title
                new Resource[] {  
                    new Resource(1500, 1), //health
                    new Resource(100, 5) //stamina
                },
                new int[] { 0, 0, 1, 0, 5 }, //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.ground
                )
    {
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
