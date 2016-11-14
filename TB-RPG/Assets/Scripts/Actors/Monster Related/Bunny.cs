using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bunny : Monster
{
    
    
    public Bunny()
        : base("Orca", //Name of Monster
               "Orca", //Name of Monster's Prefab
               1,        //level
               2,        //difficulty in level
               0,        //hit accuracy
               null,     //title
                new Resource[] {  
                    new Resource(15, 1), //health
                    new Resource(100, 1) //stamina
                },
                new int[] { 0, 0, 1, 0, 0 } //charisma, cunning, dexterity, intelligence, strength
                )
    {
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
