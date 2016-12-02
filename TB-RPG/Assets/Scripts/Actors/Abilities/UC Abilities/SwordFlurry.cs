using UnityEngine;

public class SwordFlurry : SingleTargetAbility
{

    public override void showAnimation(Actor a)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public SwordFlurry() : base() { }

    public SwordFlurry(Actor Owner) : base("Sword Flurry", "Deals .5% damage as a free action - costs 0 stamina",
        "strength", .01m, 0, false, Owner, damageType.melee)
    {

    }
}
