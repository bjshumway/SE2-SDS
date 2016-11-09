using UnityEngine;

public class SharpShooter : Ability
{    
    public SharpShooter(Actor Owner) : base("Sharp Shooter", "Hitting the bullsye deals 4x damage instead of 2x damage", -1, true, Owner)
    {

    }
}
