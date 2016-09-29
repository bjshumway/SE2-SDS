// TODO: add image

public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;
    private decimal _modifier;

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

    public decimal modifier {
        get {
            return _modifier;
        }
    }

    public Ability(string name, string toolTip, decimal stamina, decimal modifier) {
        _name = name;
        _toolTip  = toolTip;
        _stamina  = stamina;
        _modifier = modifier;
    }

    public virtual void cast(Actor caster, Actor reciever) {
        caster.stamina.subtract(stamina);
    }
}
