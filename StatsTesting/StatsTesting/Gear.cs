namespace ActorNS {

    // any Item that can be equipped 
    public class Gear : Item {
        private int _level;

        // item level (not for an equip constraint, just as a way to show how good the item is)
        public int level {
            get {
                return _level;
            }
        }

        public Gear(string name, double weight, bool tradable, int level, string toolTip = "")
            : base(name, weight, tradable, toolTip) {

            _level = level;
        }
    }
}
