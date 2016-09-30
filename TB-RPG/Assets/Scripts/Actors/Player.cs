using UnityEngine;

public class Player : UserControllable {
    public decimal gold;
    public UserControllable[] theParty;
    public Follower[] followers;

    public Player() : base()
    {

        this.followers = new Follower[2];
        this.theParty = new UserControllable[3];
        this.theParty[0] = this;
        //Debug.Log("stamina: " + this.stamina.maxValue + ", " + this.stamina.value);
    }
    
    public Player(string name, int level, decimal gold, Title title = null, Resource[] resources = null, int[] statArray = null)
        : base(name, level, title, resources, statArray) {

        this.gold = gold;
    }
}
