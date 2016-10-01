using UnityEngine;
using System.Collections;

public class MonsterAttack : Ability {

    public MonsterAttack(Actor Owner) : 
                   base(null, null,
                        10, Owner)
    {

    }

    public void showAttackAnimation()
    {
        
    }

    public override void cast()
    {
        //Get the party with GameMaster.instance.thePlayer.theParty
        //Pick a character at random
        //character.dealDamage(owner.strength);
        //showAttackAnimation
        //BattleScript.instance.refreshStatDisplay(c, "health");
    }


}
