namespace ActorNS {
    public class Equips {

        private int _armor = 0;

        public int armor {
            get {
                return _armor;
            }
        }

        public Weapon weapon {
            get {
                return weapon;
            } 
            set {
                weapon = value;
            }
        }

        // TODO: replace Gear with more specified armor classes
        // TODO: add armor calculations into setters
        public Gear head {
            get {
                return head;
            }
            set {
                head = value;
            }
        }

        public Gear chest {
            get {
                return chest;
            }
            set {
                chest = value;
            }
        }

        public Gear legs {
            get {
                return legs;
            }
            set {
                legs = value;
            }
        }

        public Gear gloves {
            get {
                return gloves;
            }
            set {
                gloves = value;
            }
        }

        public Gear boots {
            get {
                return boots;
            }
            set {
                boots = value;
            }
        }

        public Equips() {

        }
    }
}
