using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull2_1 : Monster {

    public DemonSkull2_1()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               2,        //level
               1,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(150, 1), //health
                    new Resource(100, 2) //stamina
                },
                new int[] { 0, 3, 1, 0, 2 },  //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.fire //Weakness 
            ) 
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
