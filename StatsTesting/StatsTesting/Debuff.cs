namespace ActorNS {
    public class Debuff : Buff {
        private bool _dispellable;

        public bool dispellable {
            get {
                return _dispellable;
            }
        }

        public Debuff(string statName, int value, int turnsLeft, bool dispellable, string description = "")
            : base(statName, value, turnsLeft, description) {
                _dispellable = dispellable;
        }

        public void dispell() {
            if (dispellable) {
                // however we're gonna handle dispelling debuffs
            }
        }
    }
}
