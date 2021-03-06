﻿public class ChargeStrength : Ability {

    public ChargeStrength() : base() { }

    public ChargeStrength(Actor ownerOfAbility)
		: base("Charge Strength", "Increase Attack dmg by 50% (stacks up to 4 times)", 25, false, ownerOfAbility) {

    }


    public override void cast(Actor act = null)
    {
        BattleScript bs = BattleScript.instance;



        if(owner.stats["strength"].countBuff("ChargedStrength") >= 4)
        {
            //do nothing;
            BattleHints.text = "Charge Strength only stacks 4 times.";
            return;
        }

        owner.stamina.subtract(25);        
        decimal currStr = owner.stats["strength"].level;
        owner.stats["strength"].addBuff(new Buff("ChargedStrength", currStr * .50m, "Buffs Strength by 25%"));
        showAnimation(Actor.hitType.hit);
    }


    public override void showAnimation(Actor.hitType hitType)
    {

    }
}
