
// parent class for ranged, melee, and magic weapons
// TODO: add.. sprite?
using UnityEngine;

public class Weapon : Gear {
    private weaponClass _class;
    private decimal _damage;
    private decimal _accuracy;
    private decimal _special;
    private weaponType _type;

    public enum weaponClass {
        Magic,
        Melee,
        Ranged
    };

    // this is how weapon damage / accuracy will be calculated
    public enum weaponType {
        highDamage,
        highAccuracy,
        balanced
    };

    // TODO?: change this name to something better?
    public weaponClass classType {
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

    public weaponType type {
        get {
            return _type;
        }
    }

    public Weapon(string name, decimal weight, bool tradable, decimal value, int level, weaponClass classType, weaponType type, string toolTip = "")
        : base(name, weight, tradable, value, level, toolTip) {

            _class   = classType;
            _type    = type;
            _special = level * 0.1m;
            calcStats();


        invObject = Resources.Load("InventoryWeapon") as GameObject;
        invObject = GameObject.Instantiate(invObject, invObject.transform.position, invObject.transform.rotation) as GameObject;
        GameObject scrollView = ShopInventoryScript.instance.WeaponsBuyScrollView;
        Transform tr = scrollView.transform;
        GameObject content = tr.FindChild("Viewport").FindChild("Content").gameObject;
        invObject.transform.SetParent(content.transform, false);


    }

    private void calcStats() { // formula not final of course
        if (type == weaponType.highDamage) {
            _damage   = level * 0.7m;
            _accuracy = level * 0.3m;
        } else if (type == weaponType.highAccuracy) {
            _damage   = level * 0.3m;
            _accuracy = level * 0.7m;
        } else if (type == weaponType.balanced) {
            _damage   = level * 0.5m;
            _accuracy = level * 0.5m;
        }
    }
}
