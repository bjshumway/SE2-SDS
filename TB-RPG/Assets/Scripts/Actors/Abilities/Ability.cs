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

    public Actor owner;

    public Ability(string name, string toolTip, decimal stamina, Actor ownerOfAbility) {
        _name = name;
        _toolTip  = toolTip;
        _stamina  = stamina;
        owner = ownerOfAbility;
    }

    //This function casts the ability
    //The Actor is typically reserved for whether the AI of the Monster wants to specify it.
    public virtual void cast(Actor act = null) {

    }
}
