public class Poison : SingleTargetAbility {

    public override void showAnimation(Actor a) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Poison(Actor Owner) : base("Poison", "Targetted enemy takes damage over time. Poison stacks 4 times.",
        "intellect", 0.0m, 25, false, Owner, damageType.none) {
    }

    public override void dealEffect(Actor a) {
        if (a.statusEffects["poison"] < 4) {
            base.dealEffect(a);
            a.statusEffects["poison"]++;
            a.updateStatusEffectBox();
        }
        else
        {
            BattleHints.text = "Poison only stacks 4 times";
        }
    }
}