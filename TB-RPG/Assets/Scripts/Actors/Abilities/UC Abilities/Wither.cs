public class Wither : SingleTargetAbility {

    public override void showAnimation(Actor a) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Wither(Actor Owner) : base("Wither", "Targetted enemy regens stamina at a slower rate",
        "intellect", 0.0m, 100, false, Owner, damageType.none) {
    }

    public override void dealEffect(Actor a) {
        if (!a.statusEffects["wither"]) {
            base.dealEffect(a);
            a.statusEffects["wither"] = true;
        }
    }
}