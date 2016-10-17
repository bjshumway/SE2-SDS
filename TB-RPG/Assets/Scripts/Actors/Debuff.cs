public class Debuff : Buff {
    private bool _dispellable;

    public bool dispellable {
        get {
            return _dispellable;
        }
    }

    public Debuff(string name, decimal value, bool dispellable, string description = "")
        : base(name, value, description) {

        _dispellable = dispellable;
    }

    public void dispell() {
        if (dispellable) {
            // however we're gonna handle dispelling debuffs
        }
    }
}
