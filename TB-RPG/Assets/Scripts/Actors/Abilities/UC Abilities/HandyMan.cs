using UnityEngine;

//Deprecated - we no longer have items functionality.. for now.
public class HandyMan : Ability
{
    public HandyMan() : base() { }

    public HandyMan(Actor Owner) : base("HandyMan", "Using an item now costs 5 stamina, instead of 100 stamina", -1, true, Owner)
    {

    }
}
