using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bunny : Monster
{
    
    
    public Bunny()
        : base("Bunny", //Name of Monster
               "Bunny", //Name of Monster's Prefab
               1,        //level
               2,        //difficulty in level
               0,        //hit accuracy
               true,    //isBoss
               null,     //title
                new Resource[] {  
                    new Resource(10000, 1), //health
                    new Resource(100, 5) //stamina
                },
                new int[] { 0, 4, 1, 0, 10 }, //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.ground
                )
    {
        abilities = new Ability[] { new MonsterAttack(this) };
        isFinalBoss = true;

    }



}
