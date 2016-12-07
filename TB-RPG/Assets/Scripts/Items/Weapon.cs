
// parent class for ranged, melee, and magic weapons

public class Weapon : Gear {
    public WeaponClass _class;
    public decimal _damage;
    public decimal _accuracy;
    public decimal _special;
    public WeaponType _weaponType;

    public WeaponClass weaponClass {
        get {
            return _class;
        } set {
            _class = value;
        }
   }

    public bool isEquipped;

    public enum WeaponClass {
        Magic,
        Melee,
        Ranged
    };

    // this is how weapon damage / accuracy will be calculated
    public enum WeaponType {
        highDamage,
        highAccuracy,
        balanced
    };

    // TODO?: change this name to something better?
    public WeaponClass classType {
        get {
            return _class;
        }
    }

    public decimal damage {
        get {
            return _damage;
        }
    }

    public decimal accuracy {
        get {
            return _accuracy;
        }
    }

    // this is the specialized stat for each weapon type
    // ex: Melee -> dodge rating bonus
    public decimal special {
        get {
            return _special;
        }
    }

    public WeaponType type {
        get {
            return _weaponType;
        }
    }

    public Weapon() : base()
    {

    }

    public Weapon(string name, decimal weight, bool tradable, decimal value, int level, WeaponClass classType, WeaponType weaponType, string toolTip = "")
        : base(name, weight, tradable, value, level, itemTypes.weapon, toolTip) {

            _class   = classType;
            _weaponType    = weaponType;

            _special = level * 0.1m;
        

            calcStats();



    }

    public void calcStats() { // formula not final of course
        if (type == WeaponType.highDamage) {
            _damage   = level * 0.7m;
            _accuracy = level * 0.3m;
        } else if (type == WeaponType.highAccuracy) {
            _damage   = level * 0.3m;
            _accuracy = level * 0.7m;
        } else if (type == WeaponType.balanced) {
            _damage   = level * 0.5m;
            _accuracy = level * 0.5m;
        }
    }
}
