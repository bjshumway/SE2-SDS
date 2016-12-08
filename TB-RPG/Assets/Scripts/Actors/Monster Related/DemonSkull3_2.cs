using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull3_2 : Monster {

    public DemonSkull3_2()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               3,        //level
               2,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(500, 1), //health
                    new Resource(100, 3) //stamina
                },
                new int[] { 0, 0, 1, 0, 3 },  //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.lightning //Weakness 
            ) 
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
