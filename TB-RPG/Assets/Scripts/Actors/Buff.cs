
// Stat Buff
// TODO: add image (don't know how Unity will want this)
// TODO: add turn event handler?? - to decrement turnsLeft
public class Buff {
    private int _value;
    private int _turnsLeft;
    private string _statName;
    private string _description;

    // stat buff value
    public int value {
        get {
            return _value;
        }
    }

    public int turnsLeft {
        get {
            return _turnsLeft;
        }
    }

    // strength, intellect, dexterity, cunning, charisma
    public string statName {
        get {
            return _statName;
        }
    }

    public string description {
        get {
            return _description;
        }
    }

    public Buff(string statName, int value, int turnsLeft, string description = "") {
        _statName  = statName;
        _value     = value;
        _turnsLeft = turnsLeft;

        if (description == "") { // no desc, go default
            _description = "+1 " + statName; // +1 strength
        } else {
            _description = description;
        }
    }
}
