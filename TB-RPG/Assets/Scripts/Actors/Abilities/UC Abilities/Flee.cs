public class Flee : Ability {

    public Flee() : base() { }

    public Flee(Actor Owner) : base("Flee", "Run from the battle", 100, false, Owner) {

    }

    public override void cast(Actor act = null) {

        BGM.instance.setMusic(BGM.SongNames.victory);

        // heal the caster to full
        owner.heal(owner.health.maxValue);
        owner.battleHealthBar.value = owner.battleHealthBar.maxValue;
        //Todo: copy code used elsewhere for resetting other battle things like status effects... take from heal?

        // go back to overworld
        OverworldScript.instance.load();


        owner.stamina.subtract(stamina);
    }
}
