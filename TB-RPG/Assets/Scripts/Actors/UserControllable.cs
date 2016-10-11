using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// TODO: add talent system; don't know how to flesh out talentPoints yet
public abstract class UserControllable : Actor {
    private int _talentPoints;

    //Assigns the id to each UserControllable
    protected static int id_increment = 1;
    public AbilityBar abilities;


    public Sprite _headType;
    public Color32 _headColor;

    public Image battleHead;
    

    public int talentPoints {
        get {
            return _talentPoints;
        }
    }

    public Sprite headType
    {
        get;set;
    }

    public Color32 headColor
    {
        get;set;
    }

    public UserControllable() : base()
    {

        abilities = new AbilityBar(this);

        this.id = id_increment;
        id_increment++;

        battleHealthBar = GameObject.Find("Battle UC " + id + " HealthBar").GetComponent<Slider>();
        battleHealthBar.maxValue = (float)health.maxValue;
        battleHealthBar.value = (float)battleHealthBar.maxValue;
        health.sliders = new Slider[] { battleHealthBar };

        battleStaminaBar = GameObject.Find("Battle UC " + id + " StaminaBar").GetComponent<Slider>();
        battleStaminaBar.maxValue = (float)stamina.maxValue;
        stamina.sliders = new Slider[] { battleStaminaBar };


        battleHead = GameObject.Find("Battle UC " + this.id + " HeadType").GetComponent<Image>();

        


        isUserControllable = true;


    }
}
