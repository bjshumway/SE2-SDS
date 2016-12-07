using UnityEngine;

//Deprecated - we no longer have items functionality.. for now.
public class ValueCrafter : Ability
{
    public ValueCrafter() : base() { }

    public ValueCrafter(Actor Owner) : base("Value Crafter", "Loot items (not weapons) sell for +200% their value", -1, true, Owner)
    {

    }
}
