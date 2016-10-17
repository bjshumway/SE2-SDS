﻿using UnityEngine;

public class SwordFlurry : SingleTargetDamageAbility
{

    public override void showAttackAnimation(Monster m)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public SwordFlurry(Actor Owner) : base("Sword Flurry", "Deals 5% Damage",
        "strength", .05m, 0, Owner)
    {

    }
}