public class MeleeAbility : Ability {
    public MeleeWeapon weapon;

    public MeleeAbility(string name, string toolTip, MeleeWeapon weapon) 
        : base(name, toolTip) {
            this.weapon = weapon;
    }

    public override decimal cast() {
        return weapon.damage * 0.75m;
    }
}