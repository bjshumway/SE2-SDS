namespace ActorNS {

    // any Item that can be equipped 
    public abstract class Gear : Item {
        private int _level;

        // item level (not for an equip constraint, just as a way to show how good the item is)
        public int level {
            get {
                return _level;
            }
        }

        public Gear(string name, decimal weight, bool tradable, decimal value, int level, string toolTip = "")
            : base(name, weight, tradable, value, toolTip) {

            _level = level;
        }
    }
}
