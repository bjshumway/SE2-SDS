using UnityEngine;

public class DoubleShot : Ability
{
    public DoubleShot() : base() { }

    public DoubleShot(Actor Owner) : base("DoubleShot", "Upgrade Bow Attack to fire two shots randomly", -1,true, Owner)
    {

    }
}
