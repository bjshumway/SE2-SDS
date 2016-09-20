
// TODO: add image
public abstract class Ability {
    private string _name;
    private string _toolTip;

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

    public Ability(string name, string toolTip) {
        _name    = name;
        _toolTip = toolTip;
    }

    public virtual decimal cast() {
        return 0.0m;
    }
}
