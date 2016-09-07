namespace ActorNS {

    // base class for all inventory objects in the game
    // TODO: add image field
    public class Item {
        private string _name;
        private string _toolTip;
        private double _weight;
        private bool _tradable;

        public string name {
            get {
                return _name;
            }
        }

        public string toolTip {
            get {
                return _toolTip;
            }
        }

        public double weight {
            get {
                return weight;
            }
        }

        public bool tradable {
            get {
                return _tradable;
            }
        }

        public Item(string name, double weight, bool tradable, string toolTip = "") {
            _name     = name;
            _weight   = weight;
            _tradable = tradable;
            _toolTip  = toolTip;
        }
    }
}
