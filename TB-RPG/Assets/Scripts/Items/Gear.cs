
// any Item that can be equipped 
public abstract class Gear : Item {
    private int _level;

    // item level (not for an equip constraint, just as a way to show how good the item is)
    public int level {
        get {
            return _level;
        }
    }

    public Gear(string name, decimal weight, bool tradable, decimal value, int level, Item.itemTypes itemType, string toolTip = "")
        : base(name, weight, tradable, itemType, value, toolTip) {

        _level = level;
    }
}
