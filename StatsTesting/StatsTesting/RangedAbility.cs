public class RangedAbility : Ability {
    public RangedWeapon weapon;

    public RangedAbility(string name, string toolTip, RangedWeapon weapon)
        : base(name, toolTip) {
            this.weapon = weapon;
    }

    public override decimal cast() {
        return weapon.damage * 0.75m;
    }
}