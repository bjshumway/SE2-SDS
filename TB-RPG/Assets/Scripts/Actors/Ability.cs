// TODO: add image

public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;

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

    public decimal stamina {
        get {
            return _stamina;
        }
    }

    public Ability(string name, string toolTip, decimal stamina) {
        _name = name;
        _toolTip = toolTip;
        _stamina = stamina;
    }

    public virtual decimal cast() {
        return 0.0m;
    }
}
