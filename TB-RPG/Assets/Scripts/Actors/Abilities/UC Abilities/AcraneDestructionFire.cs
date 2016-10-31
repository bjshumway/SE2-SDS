public class ArcaneDestructionFire : SingleTargetAbility {

    public override void showAnimation(Monster m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public ArcaneDestructionFire(Actor Owner) : base("Arcane Destruction (Fire)", "Deals 100% Fire damage",
        "intellect", 1.0m, 75, false, Owner, damageType.fire) {

    }
}