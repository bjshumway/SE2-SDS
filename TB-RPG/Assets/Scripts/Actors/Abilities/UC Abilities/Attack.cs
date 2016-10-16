using UnityEngine;

//This ability is used when a userControllable attacks a monster
//see monsterAttack for when a monster attacks a userControllable
public class Attack : SingleTargetDamageAbility {

    public override void showAttackAnimation(Monster m)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Attack(Actor Owner) : base("ATTACK", "Deals 100% damage",
        "strength", 1.0m, 75, Owner)
    {

    }
}
