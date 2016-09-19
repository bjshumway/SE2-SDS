public class MagicWeapon : Weapon {
    private decimal _critModifier;

    public decimal critModifier {
        get {
            return _critModifier;
        }
    }

    public MagicWeapon(string name, decimal weight, bool tradable, decimal value, int level, weaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, type, toolTip) {

            _critModifier = level * 0.1m;
    }
}
