using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;

    public Actor owner;
    public Text BattleHints;

    public GameObject learnButton;
    public Sprite buttonSprite;
    public int xPosSelectionMenu;
    public int yPosSelectionMenu;

    public GameObject currentAbSlot;

    public bool isLearned;



    public static Ability[] fighterAbilities = new Ability[]
    {
        new Attack(null),
        new ChargeStrength(null),
        new SwordFlurry(null),
        new Pin(null),
        new Parry(null),
        new LastChance(null)
    };

    public static Ability[] mageAbilities = new Ability[]
    {
        new ArcaneDestruction(null),
        new Heal(null),
        new Regen(null),
        new Wither(null),
        new Poison(null)
    };

    public static Ability[] rogueAbilities = new Ability[]
    {
	    new BowAttack(null),
        new SharpShooter(null),
        new HandyMan(null)
    };

    public bool isPassive;

    public enum damageType
    {
        melee,
        ranged,
        fire,
        water,
        ground,
        lightning,
        none
    }


    public string name {
        get {
            return _name;
        }
    }

    public string toolTip {
        get {
            return _toolTip;
        }
    }

    public decimal stamina {
        get {
            return _stamina;
        }
        set
        {
            _stamina = value;
        }
    }

    public Ability(string name, string toolTip, decimal stamina, bool isPassiveAbility, Actor ownerOfAbility) {
        _name = name;
        _toolTip  = toolTip;
        _stamina  = stamina;
        owner = ownerOfAbility;
        isLearned = false;
        isPassive = isPassiveAbility;

        //Get the image for this ability's button
        buttonSprite = Resources.Load<Sprite>("AbilityRelated/" + name + " BUTTON SPRITE" );
        if(buttonSprite == null)
        {
            buttonSprite = Resources.Load<Sprite>("AbilityRelated/TBD BUTTON SPRITE") as Sprite;
        }

        //Set the learnButton's image to this ability's button
        learnButton = Resources.Load("AbilityRelated/AbilityLearnButton") as GameObject;
        learnButton = GameObject.Instantiate(learnButton, learnButton.transform.position, learnButton.transform.rotation) as GameObject;
        learnButton.transform.SetParent(GameObject.Find("AbilitySelectCanvas").transform, false);
        learnButton.GetComponent<Image>().sprite = buttonSprite;
        learnButton.SetActive(false);

        //Set the text for this image to this button's name
        learnButton.GetComponentInChildren<Text>().text = name;

        //Initialize battlehints to its gameComponent
        BattleHints = GameObject.Find("BattleHints").GetComponentInChildren<Text>();




    }


    //This function casts the ability
    //The Actor is typically reserved for whether the AI of the Monster wants to specify it.
    public virtual void cast(Actor act = null) {

    }

    //Shows the animation of the ability
    //Based on the hitType, which is hit, crit, miss. (we may add more)
    public virtual void showAnimation(Actor.hitType hitType)
    {

    }

    //Called in BattleScript when this ability is loaded into the abilitySlot
    //Meant to be overridden by any child ability that wants to do anything meaningfully with it.
    public virtual void onLoad()
    {

    }

    //Called in BattleScript when this ability is unloaded
    //Meant to be overridden by any child ability that wants to do anything meaningfully with it.
    public virtual void onUnload()
    {

    }

}
