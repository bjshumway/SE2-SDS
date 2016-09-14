namespace ActorNS {

    // parent class for ranged, melee, and magic weapons
    // TODO: add.. sprite?
    public abstract class Weapon : Gear {
        private decimal _damage;
        private decimal _accuracy;
        private weaponType _type;

        // this is how weapon damage / accuracy will be calculated
        // i think this makes things more flavorful
        public enum weaponType {
            highDamage,
            highAccuracy,
            balanced
        }

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

        public weaponType type { // see All.weaponType
            get {
                return _type;
            }
        }

        public Weapon(string name, decimal weight, bool tradable, decimal value, int level, weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, toolTip) {

            _type = type;
            calcStats();
        }

        private void calcStats() { // formula not final of course
            if (type == weaponType.highDamage) {
                _damage   = level * 0.7m;
                _accuracy = level * 0.3m;
            } else if (type == weaponType.highAccuracy) {
                _damage   = level * 0.3m;
                _accuracy = level * 0.7m;
            } else if (type == weaponType.balanced) {
                _damage   = level * 0.5m;
                _accuracy = level * 0.5m;
            }
        }
    }
}
