public class Heal : SingleTargetAbility {

    public void showAnimation(Actor m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Heal(Actor Owner) : base("Heal", "Heals based on Intellect",
        "intellect", 1.0m, 100, false, Owner, damageType.none) {

    }

    public override void cast(Actor act = null)
    {
        BattleScript bs = BattleScript.instance;

        System.Collections.Generic.List <UserControllable> mems = UserControllable.getAliveMembers();

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
        act.heal(owner.stats[stat].modifier * modifier);
        act.statusEffects.Clear(); // clear ailments meaning status effects?

        owner.stamina.subtract(stamina);
        showAnimation(act);
    }
}