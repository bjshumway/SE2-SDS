// TODO: add image

public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;
    public Actor owner;


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


    public Ability(string name, string toolTip, decimal stamina, Actor ownerOfAbility) {
        _name = name;
        _toolTip  = toolTip;
        _stamina  = stamina;
        owner = ownerOfAbility;
    }

    public virtual void cast() {
        owner.stamina.subtract(stamina);
    }
}
