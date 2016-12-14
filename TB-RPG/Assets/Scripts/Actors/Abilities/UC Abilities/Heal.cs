using System.Collections.Generic;
using System.Linq;

public class Heal : SingleTargetAbility {

    public override void showAnimation(Actor m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Heal() : base() { }

    public Heal(Actor Owner) : base("Heal", "Heals 50% third of max HP, removes all status effects, revives dead characters.",
        "intellect", 1.0m, 100, false, Owner, damageType.none) {

    }

    public override void cast(Actor act = null)
    {
        BattleScript bs = BattleScript.instance;

        List<UserControllable> mems = UserControllable.getAliveMembers();

        if (mems.Count > 1)
        {
            
            //I tried changing the mouse icon, but couldn't find one I liked. - Ben
            //Cursor.SetCursor(GameMaster.instance.cursor2, new Vector2(0, 0), CursorMode.Auto);
            bs.pipeInputFunc = this.selectEnemy;

            //Tell the user to select a target
            BattleHints.text = MLH.tr("Select Target Ally");
            return;
        }
        else
        {
            Actor a = mems[0];
            dealEffect(a);
        }
    }


    public override void dealEffect(Actor act) {
        if(!act.isUserControllable)
        {
            BattleHints.text = MLH.tr("Select Target Ally");
            return;
        }
        act.heal(act.health.maxValue / 2.0m);

        // clearing all status effects
        for (int x = 0; x < act.statusEffects.Count; x++)
        {
            act.statusEffects[act.statusEffects.ElementAt(x).Key] = 0;
        }
        act.isAlive = true;
        act.updateStatusEffectBox();
        AudioControl.playSound("spell_1");
        owner.stamina.subtract(stamina);
        showAnimation(act);
    }
}