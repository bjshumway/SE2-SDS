using UnityEngine;
using System.Collections;

public class Player : UserControllable {
    public decimal gold;
    public UserControllable[] theParty;
    public Follower[] followers;

    public Player() : base()
    {

        this.followers = new Follower[2];
        this.theParty = new UserControllable[3];
        this.theParty[0] = this;
        Debug.Log("stamina: " +this.stamina.maxValue + ", " + this.stamina.value);

        Debug.Log("stamina: " + stamina.maxValue + ", " + stamina.value);
    }

    // I'm thinking now that the constructor for Actor needs to be redone.
    public Player(string name, int level, decimal gold, Title title = null, Resource[] resources = null, int[] statArray = null)
        : base(name, level, title, resources, statArray) {

        this.gold = gold;


    }
}
