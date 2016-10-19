
// Stat Buff
// TODO: add image (don't know how Unity will want this)
public class Buff {
    private decimal _value;
    private string _name;
    private string _description;
    

    // stat buff value
    public decimal value {
        get {
            return _value;
        }
    }


    // strength, intellect, dexterity, cunning, charisma
    public string name {
        get {
            return _name;
        }
    }

    public string description {
        get {
            return _description;
        }
    }

    public Buff(string name, decimal value, string description) {
        _name = name;
        _value     = value;
        _description = description;
    }
}
