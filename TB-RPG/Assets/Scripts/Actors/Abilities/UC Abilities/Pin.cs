using UnityEngine;

//This ability is used when a userControllable attacks a monster
//see monsterAttack for when a monster attacks a userControllable
public class Pin : SingleTargetAbility
{

    public override void showAnimation(Monster m)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public Pin(Actor Owner) : base("PIN", "Pins The Enemy in Place - No Longer Can Dodge",
        "strength", 0.0m, 25, Owner)
    {

    }

    //Pins the damagee
    public override void dealEffect(Monster m)
    {
        m.statusEffects["pin"] = true;
        owner.stamina.subtract(stamina);
        showAnimation(m);
    }
}
