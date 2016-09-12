namespace ActorNS {

    // parent class for ranged, melee, and magic weapons
    // TODO: add.. sprite?
    public class Weapon : Gear {
        private decimal _damage;
        private decimal _accuracy;
        private All.weaponType _type;

        public decimal damage {
            get {
                return _damage;
            }
        }

        public decimal accuracy {
            get {
                return _accuracy;
            }
        }

        public All.weaponType type { // see All.weaponType
            get {
                return _type;
            }
        }

        public Weapon(string name, decimal weight, bool tradable, decimal value, int level, All.weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, toolTip) {

            _type = type;
            calcStats();
        }

        private void calcStats() { // formula not final of course
            if (type == All.weaponType.highDamage) {
                _damage   = level * 0.7m;
                _accuracy = level * 0.3m;
            } else if (type == All.weaponType.highAccuracy) {
                _damage   = level * 0.3m;
                _accuracy = level * 0.7m;
            } else if (type == All.weaponType.balanced) {
                _damage   = level * 0.5m;
                _accuracy = level * 0.5m;
            }
        }
    }
}
