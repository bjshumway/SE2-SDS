using UnityEngine;

public abstract class SingleTargetDamageAbility : Ability {

    private decimal _modifier;
    private string _stat;

    public decimal modifier {
        get {
            return _modifier;
        }
    }

    public string stat {
        get {
            return _stat;
        }
    }


    public SingleTargetDamageAbility(string name, string toolTip, string stat, decimal modifier, decimal stamina, Actor ownerOfAbility)
		: base(name, toolTip, stamina, ownerOfAbility) {
            _modifier = modifier;
        _stat = stat;
    }

    //arg is a string of the format "typeOfThingClickedOn index"
    //e.g. "Monster 1" or "UserControllable 2" or "AbilityBar 1"
    public void selectEnemy(string arg) {
        //Debug.Log("Ran in SelectEnemy, arg: " + arg);
        //   check to see what was clicked on is a monster
        //   if it's not a monster do nothing
        string[] args = arg.Split();

        if (args[0] != "Monster") {
            return;
        } else {
            BattleScript bs = BattleScript.instance;
            Monster m = bs.monsters[int.Parse(args[1])];
            dealDamage(m);
            bs.pipeInputFunc = null;
        }
    }

    public virtual void showAttackAnimation(Monster m) {

    }

    public override void cast(Actor act = null) {
        BattleScript bs = BattleScript.instance;

        if (bs.monsters.Length > 1) {
            //todo: add bs.changeMouseIconToSelectPointer()
            bs.pipeInputFunc = this.selectEnemy;
        } else {
            Monster m = bs.monsters[0];
            dealDamage(m);
        }
    }

    /// <summary>
    /// Deals damage to the selected monster
    /// </summary>
    /// <param name="modifier">for formula (statlevel * weapondamage * modifier)</param>
    public void dealDamage(Monster m) {
        BattleScript bs = BattleScript.instance;
        
        m.damage(owner.stats[_stat].effectiveLevel * owner.weapon.damage * modifier);

        owner.stamina.subtract(stamina);


        showAttackAnimation(m);
    }
}
