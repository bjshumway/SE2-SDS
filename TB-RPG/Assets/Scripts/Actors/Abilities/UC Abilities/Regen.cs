public class Regen : Ability {

    public void showAttackAnimation(Monster m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Regen(Actor Owner) : base("Regen", "Regenerate health over time", 50, false, Owner) {
        
    }

    public override void cast(Actor act = null) {
        base.cast(act);

        if (!owner.hasPassive("Regen")) {
            owner.stamina.subtract(stamina);
            owner.passiveAbilities.Add(this);
        }
    }
}
