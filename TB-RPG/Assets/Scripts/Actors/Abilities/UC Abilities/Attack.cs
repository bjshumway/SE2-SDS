using UnityEngine;
using System;

//This ability is used when a userControllable attacks a monster
//see monsterAttack for when a monster attacks a userControllable
public class Attack : Ability {

    //arg is a string of the format "typeOfThingClickedOn index"
    //e.g. "Monster 1" or "UserControllable 2" or "AbilityBar 1"
    public void selectEnemy(string arg)
    {
        Debug.Log("Ran in SelectEnemy, arg: " + arg);
        //   check to see what was clicked on is a monster
        //   if it's not a monster do nothing
        String[] args = arg.Split();

        if (args[0] != "Monster")
        {
            return;
        }
        else
        {
            BattleScript bs = BattleScript.instance; 
            Monster m = bs.monsters[int.Parse(args[1])];
            m.damage(owner.stats["strength"].effectiveLevel * owner.weapon.damage);
            bs.refreshStatDisplay(m, "health");
            showAttackAnimation(m);
            bs.refreshStatDisplay(m, "health"); //refreshes showing all the stats of enemy monster and characters
            bs.pipeInputFunc = null;
        }
    }

    public void showAttackAnimation(Monster m)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to it's image, to make things easier
    }

    public Attack(Actor Owner) : base("ATTACK", "Deals <insert formula for str based damage>",
                            10, Owner)
    {

    }

    public override void cast()
    {
        Debug.Log("Ran in cast for attack");

        owner.stamina.subtract(stamina);
        BattleScript bs = BattleScript.instance;

        if( bs.monsters.Length > 1) {
           //todo: add bs.changeMouseIconToSelectPointer()
           bs.pipeInputFunc = this.selectEnemy;
        }
        else
        { 
           Monster m = bs.monsters[0];
           m.damage(owner.stats["strength"].effectiveLevel * owner.weapon.damage);
           bs.refreshStatDisplay(m, "health"); //refreshes showing all the stats of enemy monster and characters
           showAttackAnimation(bs.monsters[0]);
        }
    }

}
