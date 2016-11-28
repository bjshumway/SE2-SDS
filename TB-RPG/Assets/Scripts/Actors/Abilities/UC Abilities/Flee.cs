public class Flee : Ability {

    public Flee(Actor Owner) : base("Flee", "Run from the battle", 100, false, Owner) {

    }

    public override void cast(Actor act = null) {

        // heal the caster to full
        owner.heal(owner.health.maxValue);
        owner.battleHealthBar.value = owner.battleHealthBar.maxValue;

        // go back to overworld
        GameMaster.instance.switchCamera(4);

        owner.stamina.subtract(stamina);
    }
}
