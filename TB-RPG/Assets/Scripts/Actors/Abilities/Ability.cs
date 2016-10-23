using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Ability {
    private string _name;
    private string _toolTip;
    private decimal _stamina;

    public Actor owner;

    public GameObject learnButton;
    public Sprite buttonSprite;
    public int xPosSelectionMenu;
    public int yPosSelectionMenu;

    public bool isLearned;



    public static Ability[] fighterAbilities = new Ability[]
    {
        new Attack(null),
        new ChargeStrength(null),
        new SwordFlurry(null),
        new Pin(null),
        new Parry(null)
    };

    public static Ability[] mageAbilities = new Ability[]
    {

    };

    public static Ability[] rogueAbilities = new Ability[]
    {

    };

    public bool isPassive;

    public enum damageType
    {
        melee,
        ranged,
        fire,
        water,
        ground,
        lightning
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
    }

    public Ability(string name, string toolTip, decimal stamina, bool isPassiveAbility, Actor ownerOfAbility) {
        _name = name;
        _toolTip  = toolTip;
        _stamina  = stamina;
        owner = ownerOfAbility;
        isLearned = false;
        isPassive = isPassiveAbility;

        //Get the image for this ability's button
        buttonSprite = Resources.Load<Sprite>(name + " BUTTON SPRITE" );
        if(buttonSprite == null)
        {
            buttonSprite = Resources.Load<Sprite>("TBD BUTTON SPRITE") as Sprite;
        }

        //Set the learnButton's image to this ability's button
        learnButton = Resources.Load("AbilityLearnButton") as GameObject;
        learnButton = GameObject.Instantiate(learnButton, learnButton.transform.position, learnButton.transform.rotation) as GameObject;
        learnButton.transform.SetParent(GameObject.Find("AbilitySelectCanvas").transform, false);
        learnButton.GetComponent<Image>().sprite = buttonSprite;
        learnButton.SetActive(false);

        //Set the text for this image to this button's name
        learnButton.GetComponentInChildren<Text>().text = name;





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
}
