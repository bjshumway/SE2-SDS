using UnityEngine;

public class Player : UserControllable {
    public decimal gold;
    public UserControllable[] theParty;
    public Follower[] followers;
    public bool partyIsDead;

    public Player() : base()
    {

        this.followers = new Follower[2];
        this.theParty = new UserControllable[3];
        this.theParty[0] = this;
        partyIsDead = false;
        //Debug.Log("stamina: " + this.stamina.maxValue + ", " + this.stamina.value);

    }
}
