public class RangedAbility : Ability {

    public RangedAbility(string name, string toolTip, decimal stamina, decimal modifier)
        : base(name, toolTip, stamina, modifier) {
    }

    public override void cast(Actor caster, Actor reciever) {
        base.cast(caster, reciever);
        reciever.damage(caster.stats["dexterity"].modifier * modifier);
    }
}