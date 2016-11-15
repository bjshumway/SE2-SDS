﻿public class Poison : SingleTargetAbility {

    public override void showAnimation(Actor a) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Poison(Actor Owner) : base("Poison", "Targetted enemy takes damage over time",
        "intellect", 0.0m, 50, false, Owner, damageType.none) {
    }

    public override void dealEffect(Actor a) {
        if (!a.statusEffects["poison"]) {
            base.dealEffect(a);
            a.statusEffects["poison"] = true;
        }
    }
}