public class MagicAbility : Ability {
    public MagicWeapon weapon;

    public MagicAbility(string name, string toolTip, MagicWeapon weapon)
        : base(name, toolTip) {
            this.weapon = weapon;
    }

    public override decimal cast() {
        return weapon.damage * 0.75m;
    }
}