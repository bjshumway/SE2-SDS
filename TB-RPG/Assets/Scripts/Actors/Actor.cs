using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO:? Make resources (h/m/s) scale from level? Eliminate mana/stamina?
// Actor is the class from which all characters will inherit
public class Actor {

    #region Private Vars

    private string _name;
    private string _fullName;
    private Title  _title;

    private bool _isAlive = true;
    private int  _level = 1;


    public int id; //unique across all monsters and actors
    public bool isUserControllable;

    public Slider battleHealthBar;
    public Slider battleStaminaBar;

    #endregion

    #region Public Vars

    public string name {
        get {
            return _name;
        }
    }

    // name + title or title + name
    public string fullName {
        get {
            return _fullName;
        }
    }

    // just added this in for fun, see Title.cs (Title as in Dr. or President or .. THE UNKILLABLE)
    public Title title {
        get {
            return _title;
        }
    }

    public bool isAlive {
        get {
            return _isAlive;
        }
    }

    public int level {
        get {
            return _level;
        }
    }

    public Weapon weapon;

    public Resource health;
    public Resource stamina;

    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    #endregion

    #region Constructor & Methods

    //simple constructor, used primarilly by Monsters
    public Actor(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null) {
        _name = name;
        _level = level;

        _isAlive = true;

        if (title != null) { // title
            setTitle(title);
        } else { // no title
            _fullName = name;
        }

        if (resources != null) { // h/m/s specified
            health  = resources[0];
            stamina = resources[1];
        } else { // h/m/s not specified - go by this formula
            int resourceModifier = 10 * level; // no idea if this formula will be good
            health  = new Resource(resourceModifier);
            stamina = new Resource(100); //Stamina should alway max out at 100.
        }

        // setting up the default stats
        stats.Add("strength",  new Stat(1, 0));
        stats.Add("intellect", new Stat(1, 0));
        stats.Add("dexterity", new Stat(1, 0));
        stats.Add("cunning",   new Stat(1, 0));
        stats.Add("charisma",  new Stat(1, 0));

        if (statArray != null) { // stats specified
            setStatLevels(statArray);
        }
    }

    // Simple constructor (used primarilly by userControllables)
    public Actor() {
        decimal resourceModifier = 10; // no idea if this formula will be good
        health = new Resource(resourceModifier, -1);
        stamina = new Resource(100,-1);

        //Debug.Log("max stamina in player is " + stamina.maxValue);

        _isAlive = true;

        // setting up the default stats
        stats.Add("strength", new Stat(1, 0));
        stats.Add("intellect", new Stat(1, 0));
        stats.Add("dexterity", new Stat(1, 0));
        stats.Add("cunning", new Stat(1, 0));
        stats.Add("charisma", new Stat(1, 0));

    }

    /// <summary>
    /// Sets Actor.title with the specified Title, and applies the title to Actor.fullName
    /// </summary>
    /// <param name="newTitle">Title to apply</param>
    public void setTitle(Title newTitle) {
        _title = newTitle;

        if (title.beforeName) {
            _fullName = title.text + " " + name;
        } else {
            _fullName = name + " " + title.text;
        }
    }

    /// <summary>
    /// Clears the current title, setting Actor.title to null, and Actor.fullName to Actor.name
    /// </summary>
    public void clearTitle() {
        _title = null;
        _fullName = name;
    }

    /// <summary>
    /// Sets all stats to the specified values
    /// </summary>
    /// <param name="stats">
    /// Array of ints corresponding to the Actor's stats in order:
    /// strength, intellect, dexterity, cunning, charisma 
    /// The order of stats is in alphabetical order:  charisma, cunning, dexterity, intellect, strength
    /// </param>

    public void setStatLevels(int[] newStats) {
        stats["charisma"].setLevel(newStats[0]);
        stats["cunning"].setLevel(newStats[1]);
        stats["dexterity"].setLevel(newStats[2]);
        stats["intellect"].setLevel(newStats[3]);
        stats["strength"].setLevel(newStats[4]);        
    }

    /// <summary>
    /// Sets all stats to the specified value
    /// </summary>
    /// <param name="newStats">int to set ALL stat values to</param>
    public void setStatLevels(int newStats) {
        foreach (KeyValuePair<string, Stat> stat in stats) {
            stat.Value.setLevel(newStats);
        }
    }

    /// <summary>
    /// Brings the actor back to life with specified percentage of resources
    /// </summary>
    /// <param name="percentResources">
    /// Percentage of health/mana/stamina Actor is res'd with (1.0 for 100%)
    /// </param>
    public void resurrect(decimal percentResources) {
        health.setValue(health.maxValue * percentResources);
        stamina.setValue(stamina.maxValue * percentResources);

        _isAlive = true;
    }

    //Shows the animation for the monster / player dying
    //Todo: make _isAlive false After the animation is finished
    public void showDeathAnimation()
    {

    }

    /// <summary>
    /// Kills the Actor, setting all resources to 0
    /// It also calls showDeathAnimation
    /// It also checks to see if the party is dead,
    /// if the party is dead it updates partyIsDead variable in player
    /// </summary>
    public virtual void kill() {
        health.setValue(0);
        stamina.setValue(0);

        showDeathAnimation();

        _isAlive = false;
        if(isUserControllable)
        {
            Actor[] party = GameMaster.instance.thePlayer.theParty;
            bool everyoneDied = true;
            for(int i = 0; i < party.Length; i++)
            {
                if(party[i] != null && party[i].isAlive == true) {
                    everyoneDied = false;
                }
            }
            if(everyoneDied)
            {
                GameMaster.instance.thePlayer.partyIsDead = true;
            }
        }
    }

    /// <summary>
    /// Attempts to damage the actor by the specified amount
    /// </summary>
    /// <param name="damageAmount">amount to damage</param>
    /// <returns>true if damaged, false if actor dodged</returns>
    public bool damage(decimal damageAmount) {
        System.Random ran = new System.Random();
        decimal dodgeRoll = ran.Next(0, 100) / 100m;

        if (stats["cunning"].modifier * 0.5m < dodgeRoll) {
            health.subtract(damageAmount); // ouch

            if (health.value == 0) {
                kill(); // yo dead
            }

            return true;
        }

        return false; // dodged
    }

    /// <summary>
    /// Heals the Actor by the specified amount
    /// </summary>
    /// <param name="healAmount">Amount to heal</param>
    public void heal(decimal healAmount) {
        health.add(healAmount);
    }

    /// <summary>
    /// Drains the specified amount of stamina
    /// </summary>
    /// <param name="amount">Amount of stamina to drain</param>
    public void drainStamina(decimal amount) {
        stamina.subtract(amount);
    }

    #endregion
}
