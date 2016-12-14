using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull1_3 : Monster {

    public DemonSkull1_3()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               1,        //level
               3,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(15, 1), //health
                    new Resource(100, 1) //stamina
                },
                new int[] { 0, 2, 1, 0, 0 },  //charisma, cunning, dexterity, intelligence, strength
                Ability.damageType.water //Weakness 
            ) 
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
