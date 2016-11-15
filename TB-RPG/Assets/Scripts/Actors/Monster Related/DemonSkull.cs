﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonSkull : Monster {

    public DemonSkull()
        : base("DemonSkull", //Name of Monster
               "demonSkull", //Name of Monster's Prefab
               1,        //level
               1,        //difficulty in level
               0,        //hit accuracy
               false,    //isBoss
               null,     //title
                new Resource[] {
                    new Resource(15, 1), //health
                    new Resource(100, 1) //stamina
                },
                new int[] { 0, 0, 1, 0, 0 }) //charisma, cunning, dexterity, intelligence, strength
    {

        //Setup
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
