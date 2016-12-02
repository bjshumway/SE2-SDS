public class RangedWeapon : Weapon {
    public decimal _firstShotModifier;

    public decimal firstShotModifier {
        get {
            return _firstShotModifier;
        }
    }

    public RangedWeapon() : base()
    {

    }

    public RangedWeapon(string name, decimal weight, bool tradable, decimal value, int level, weaponClass classTyp, weaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, classTyp, type, toolTip) {

            _firstShotModifier = level * 0.1m;
    }
}
