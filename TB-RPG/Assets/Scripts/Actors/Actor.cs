using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// TODO:? Make resources (h/m/s) scale from level?
// Actor is the class from which all characters will inherit
public class Actor {

    #region Private Vars

    private string _name;
    private string _fullName;
    private Title _title;

    private bool _isAlive = true;
    private int _level = 1;

    #endregion

    #region Public Vars

    public new string name
    {
        get
        {
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

    // went with a skyrim-type system here
    public Resource health;
    public Resource stamina;

    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    #endregion

    #region Constructor & Methods


    //Simple constructor
    public Actor()
    {
        double resourceModifier = 10; // no idea if this formula will be good
        health = new Resource(resourceModifier);
        stamina = new Resource(resourceModifier, 0);

        Debug.Log("maxsamina in player is " + stamina.maxValue);


        // setting up the default stats
        stats.Add("strength", new Stat(1, 0));
        stats.Add("intellect", new Stat(1, 0));
        stats.Add("dexterity", new Stat(1, 0));
        stats.Add("cunning", new Stat(1, 0));
        stats.Add("charisma", new Stat(1, 0));

    }

    /// <summary>
    /// Constructor for Actor
    /// </summary>
    /// <param name="name">Name of the Actor</param>
    /// <param name="title">Title for the Actor, if any</param>
    /// <param name="resources">
    /// Array of Resources corresponding to health, stamina
    /// </param>
    /// <param name="statArray">
    /// Array of ints corresponding to the Actor's stats in order:
    /// strength, intellect, dexterity, cunning, charisma
    /// </param>
    public Actor(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null) {
        _name = name;
        _level = level;

        if (title != null) { // title
            setTitle(title);
        } else { // no title
            _fullName = name;
        }

        if (resources != null) { // h/m/s specified
            health  = resources[0];
            stamina = resources[2];
        } else { // h/m/s not specified - go by this formula
            int resourceModifier = 100 + (level * 7); // no idea if this formula will be good
            health  = new Resource(resourceModifier);
            stamina = new Resource(resourceModifier);
        }

        // setting up the default stats
        stats.Add("strength",  new Stat(1, 0));
        stats.Add("intellect", new Stat(1, 0));
        stats.Add("dexterity", new Stat(1, 0));
        stats.Add("cunning",   new Stat(1, 0));
        stats.Add("charisma",  new Stat(1, 0));
        // do we add armor?

        if (statArray != null) { // stats specified
            setStatLevels(statArray);
        }
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
    /// </param>
    public void setStatLevels(int[] newStats) {
        int x = 0;
        foreach (KeyValuePair<string, Stat> stat in stats) {
            stat.Value.setLevel(newStats[++x]);
        }
        
    }

    /// <summary>
    /// Sets all stats to the specified value
    /// </summary>
    /// <param name="newStats">int to set ALL stat values to</param>
    public void setStatsLevels(int newStats) {
        foreach (KeyValuePair<string, Stat> stat in stats) {
            stat.Value.setLevel(newStats);
        }
    }

    /// <summary>
    /// Brings the actor back to life with specified percentage of resources
    /// </summary>
    /// <param name="percentResources">
    /// Percentage of health/stamina Actor is res'd with (1.0 for 100%)
    /// </param>
    public void resurrect(double percentResources) {
        health.setValue(health.maxValue * percentResources);
        stamina.setValue(stamina.maxValue * percentResources);

        _isAlive = true;
    }

    /// <summary>
    /// Kills the Actor, setting all resources to 0
    /// </summary>
    public void kill() {
        health.setValue(0);
        stamina.setValue(0);

        _isAlive = false;

        // TODO: whatever we want to do on death goes here
    }

    // TODO: finish this method
    public void damage(double damageAmount) {
        // dodge roll here?
        // armor reduction here?

        health.subtract(damageAmount);
    }

    /// <summary>
    /// Heals the Actor by the specified amount
    /// </summary>
    /// <param name="healAmount">Amount to heal</param>
    public void heal(double healAmount) {
        health.add(healAmount);
    }

    
    /// <summary>
    /// Drains the specified amount of stamina
    /// </summary>
    /// <param name="amount">Amount of stamina to drain</param>
    public void drainStamina(double amount) {
        stamina.subtract(amount);
    }

    #endregion
}
