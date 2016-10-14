﻿using UnityEngine;
using System.Collections;

public class MonsterAttack : Ability {

    public MonsterAttack(Actor Owner) : 
                   base(null, null,
                        100, Owner)
    {

    }

    public void showAttackAnimation()
    {
        
    }

    public override void cast(Actor act = null)
    {


       if (act == null)
        {
            //Get the party with GameMaster.instance.thePlayer.theParty
            Actor[] party = GameMaster.instance.thePlayer.theParty;
            //Pick a character at random
            System.Random rand = new System.Random();


            while (GameMaster.instance.thePlayer.partyIsDead == false)
            {
                int k = rand.Next(0, party.Length -1);
                if (party[k] != null && party[k].isAlive)
                {
                    act = party[k];
                    break;
                }
            }
        }

        act.damage(owner.stats["strength"].effectiveLevel);
        owner.stamina.subtract(stamina);


    }


}