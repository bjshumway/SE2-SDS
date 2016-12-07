public class MeleeWeapon : Weapon {
    public decimal _parryChance;

    public decimal parryChance {
        get {
            return _parryChance;
        }
    }

    public MeleeWeapon(): base(){

    }

    public MeleeWeapon(string name, decimal weight, bool tradable, decimal value, int level, WeaponClass classType, WeaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, classType, type, toolTip) {

            _parryChance = level * 0.1m;
    }
}
