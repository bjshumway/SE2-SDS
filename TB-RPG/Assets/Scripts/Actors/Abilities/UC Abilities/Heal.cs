public class Heal : SingleTargetAbility {

    public void showAnimation(Actor m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Heal(Actor Owner) : base("Heal", "Heals based on Intellect",
        "intellect", 1.0m, 50, false, Owner, damageType.none) {

    }

    public void dealEffect(Actor act) {
        act.heal(owner.stats[stat].modifier * modifier);
        act.statusEffects.Clear(); // clear ailments meaning status effects?

        owner.stamina.subtract(stamina);
        showAnimation(act);
    }
}