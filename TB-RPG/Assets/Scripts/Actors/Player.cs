using UnityEngine;

public class Player : UserControllable {
    public decimal gold;
    public UserControllable[] theParty;
    public Follower[] followers;
    public bool partyIsDead;
    public Inventory inventory;

    public Player() : base()
    {
        this.followers = new Follower[2];
        this.theParty = new UserControllable[3];
        this.theParty[0] = this;
        this.partyIsDead = false;

        this.name = "Trifaldo";
        //Debug.Log("stamina: " + this.stamina.maxValue + ", " + this.stamina.value);

        inventory = new Inventory(this, "Inventory", 10);

    }

    //Adds a party member to the party!
    public void addPartyMember()
    {
        Follower newMember = new Follower();

        followers[newMember.id - 2] = newMember;
        theParty[newMember.id - 1] = newMember;

        CharacterCreationMenu.load(newMember, false);
    }
}
