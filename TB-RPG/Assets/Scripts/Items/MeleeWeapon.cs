namespace ActorNS {
    public class MeleeWeapon : Weapon {
        private decimal _parryChance;

        public decimal parryChance {
            get {
                return _parryChance;
            }
        }

        public MeleeWeapon(string name, decimal weight, bool tradable, decimal value, int level, weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, type, toolTip) {

                _parryChance = level * 0.1m;
        }
    }
}
