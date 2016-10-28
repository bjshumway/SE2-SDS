using UnityEngine;

public abstract class SingleTargetAbility : Ability {

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


    public SingleTargetAbility(string name, string toolTip, string stat, decimal modifier, decimal stamina, bool isPassiveAbility, Actor ownerOfAbility)
		: base(name, toolTip, stamina, isPassiveAbility, ownerOfAbility) {
            _modifier = modifier;
        _stat = stat;
    }

    //arg is a string of the format "typeOfThingClickedOn index"
    //e.g. "Monster 1" or "UserControllable 2" or "AbilityBar 1"
    public void selectEnemy(string arg) {

        Debug.Log("Ran in SelectEnemy, arg: " + arg);
        
        //   check to see what was clicked on is a monster
        //   if it's not a monster do nothing
        string[] args = arg.Split();

        if (args[0] != "Monster") {
            return;
        } else {
            BattleScript bs = BattleScript.instance;
            Monster m = bs.monsters[int.Parse(args[1]) -1];
            dealEffect(m);
            bs.pipeInputFunc = null;
            //Uncomment below if we want to change the mouse back to regular
            //Cursor.SetCursor(GameMaster.instance.cursor1, new Vector2(0, 0), CursorMode.Auto);
        }
    }

    public virtual void showAnimation(Monster m) {

    }

    public override void cast(Actor act = null) {
        BattleScript bs = BattleScript.instance;


        if (bs.monsters.Length > 1) {
            int aliveCount = 0;
            Monster aliveMonster = null;
            for(int i = 0; i < bs.monsters.Length; i++)
            {
                if(bs.monsters[i].isAlive)
                {
                    aliveCount++;
                    aliveMonster = bs.monsters[i];
                }
            }
            if (aliveCount > 1)
            {
                //I tried changing the mouse icon, but couldn't find one I liked. - Ben
                //Cursor.SetCursor(GameMaster.instance.cursor2, new Vector2(0, 0), CursorMode.Auto);
                bs.pipeInputFunc = this.selectEnemy;
                return;
            } else
            {
                dealEffect(aliveMonster);
            }
        } else {
            Monster m = bs.monsters[0];
            dealEffect(m);
        }
    }

    /// <summary>
    /// Deals damage to the selected monster
    /// </summary>
    /// <param name="modifier">for formula (statlevel * weapondamage * modifier)</param>
    public virtual void dealEffect(Monster m) {
        m.damage(owner.stats[_stat].effectiveLevel * owner.weapon.damage * modifier, owner, Ability.damageType.melee);

        owner.stamina.subtract(stamina);

        showAnimation(m);
    }
}
