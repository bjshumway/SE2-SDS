namespace ActorNS {

    // base class for ranged, melee, and magic weapons
    public class Weapon : Gear {
        private double _damage;
        private double _accuracy;
        private All.weaponType _type;
        

        public double damage {
            get {
                return _damage;
            }
        }

        public double accuracy {
            get {
                return _accuracy;
            }
        }

        public All.weaponType type { // see All.weaponType
            get {
                return _type;
            }
        }

        public Weapon(string name, double weight, bool tradable, int level, All.weaponType type, string toolTip = "")
            : base(name, weight, tradable, level, toolTip) {

            _type = type;
            calcStats();
        }

        private void calcStats() { // formula not final of course
            if (type == All.weaponType.highDamage) {
                _damage   = level * 0.7;
                _accuracy = level * 0.3;
            } else if (type == All.weaponType.highAccuracy) {
                _damage   = level * 0.3;
                _accuracy = level * 0.7;
            } else if (type == All.weaponType.balanced) {
                _damage   = level * 0.5;
                _accuracy = level * 0.5;
            }
        }
    }
}
