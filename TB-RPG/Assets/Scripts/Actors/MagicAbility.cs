public class MagicAbility : Ability {

    public MagicAbility(string name, string toolTip, decimal stamina, decimal modifier)
        : base(name, toolTip, stamina, modifier) {
    }

    public override void cast(Actor caster, Actor reciever) {
        base.cast(caster, reciever);
        reciever.damage(caster.weapon.damage * caster.stats["intellect"].modifier * modifier);
    }
}