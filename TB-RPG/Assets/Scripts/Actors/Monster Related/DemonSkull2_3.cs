using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull2_3 : Monster {

    public DemonSkull2_3()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               2,        //level
               3,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(150, 1), //health
                    new Resource(100, 2) //stamina
                },
                new int[] { 0, 0, 1, 0, 2 },  //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.water //Weakness 
            ) 
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
