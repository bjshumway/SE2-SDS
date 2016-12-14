using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull3_1 : Monster {

    public DemonSkull3_1()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               3,        //level
               1,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(500, 1), //health
                    new Resource(100, 3) //stamina
                },
                new int[] { 0, 3, 1, 0, 3 },  //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.fire //Weakness 
            ) 
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
