public class Wither : SingleTargetAbility {

    public override void showAnimation(Actor a) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Wither() : base() { }

    public Wither(Actor Owner) : base("Wither", "Targetted enemy regens stamina at a slower rate. Stacks up to 4 times.",
        "intellect", 0.0m, 25, false, Owner, damageType.none) {
    }

    public override void dealEffect(Actor a) {
        if (a.statusEffects["wither"] < 4)
        {
            base.dealEffect(a);
            a.statusEffects["wither"]++;
            a.updateStatusEffectBox();
        }
        else
        {
            BattleHints.text = "Wither only stacks 4 times.";
        }
    }
}