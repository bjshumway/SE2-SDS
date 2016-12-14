using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Orca : Monster
{
    
    
    public Orca()
        : base("Orca", //Name of Monster
               "Orca", //Name of Monster's Prefab
               1,        //level
               2,        //difficulty in level
               0,        //hit accuracy
               true,
               null,     //title
                new Resource[] {  
                    new Resource(500, 1), //health
                    new Resource(100, 5) //stamina
                },
                new int[] { 0, 0, 1, 0, 1 }, //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.lightning
                )
                
    {
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
