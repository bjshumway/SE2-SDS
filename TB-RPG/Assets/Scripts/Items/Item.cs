
// TODO: add image field
public class Item :InventoryObject {
    private string _name;
    private string _toolTip;
    private decimal _weight;
    private bool _tradable;
    private decimal _value;

    public enum itemTypes
    {
        weapon,
        abilityItem,
        loot
    }

    //The type of item it is: weapon, abilityItem, loot
    public itemTypes itemType;

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

    public decimal weight {
        get {
            return _weight;
        }
    }

    public bool tradable {
        get {
            return _tradable;
        }
    }

    public decimal value { // gold value of the item
        get {
            return _value;
        }
    }

    public Item() : base()
    {

    }

    public Item(string name, decimal weight, bool tradable, itemTypes itemType, decimal value = 1, string toolTip = "") {
        _name     = name;
        _weight   = weight;
        _tradable = tradable;
        _value    = value;
        _toolTip  = toolTip;
        this.itemType = itemType;
    }

    public override string ToString() {
        return name;
    }
}
