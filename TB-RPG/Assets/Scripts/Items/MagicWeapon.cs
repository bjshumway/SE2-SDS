public class MagicWeapon : Weapon {
    public decimal _critModifier;

    public decimal critModifier {
        get {
            return _critModifier;
        }
    }

    public MagicWeapon(): base(){

    }


    public MagicWeapon(string name, decimal weight, bool tradable, decimal value, int level, WeaponClass classType, WeaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, classType, type, toolTip) {

            _critModifier = level * 0.1m;
    }
}
