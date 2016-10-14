public class RangedWeapon : Weapon {
    private decimal _firstShotModifier;

    public decimal firstShotModifier {
        get {
            return _firstShotModifier;
        }
    }

    public RangedWeapon(string name, decimal weight, bool tradable, decimal value, int level, weaponClass classType, weaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, classType, type, toolTip) {

            _firstShotModifier = level * 0.1m;
    }
}
