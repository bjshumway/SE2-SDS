public class LastChance : Ability {

    public void showAttackAnimation(Monster m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public LastChance() : base() { }

    public LastChance(Actor Owner) : base("Last Chance", "Deal 3x damage when you have under 10 percent HP", -1, true, Owner)
    {

    }
}
