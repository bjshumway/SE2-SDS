public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;

    public Actor owner;

    public enum damageType
    {
        melee,
        ranged,
        fire,
        water,
        ground,
        lightning
    }

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

    //This function casts the ability
    //The Actor is typically reserved for whether the AI of the Monster wants to specify it.
    public virtual void cast(Actor act = null) {

    }

    //Shows the animation of the ability
    //Based on the hitType, which is hit, crit, miss. (we may add more)
    public virtual void showAnimation(Actor.hitType hitType)
    {

    }
}
