using UnityEngine;
using System.Collections;

public class ItemAbility : Ability {
    
    public ItemAbility(Actor owner) : base("ITEM", "Click to use an item", 100, false, owner)
    {

    }
}
