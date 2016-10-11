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
        battleStaminaBar = GameObject.Find("Battle UC " + id + " StaminaBar").GetComponent<Slider>();

        battleHealthBar.onValueChanged.AddListener(
        delegate {
            //Debug.Log("change noticed in healthbar: "+ "Battle UC " + id + " HealthBar");
            BattleScript.instance.updateResourceBarText("Battle UC " + id + " HealthBar");
        });

        battleHealthBar.maxValue = (float)health.maxValue;
        battleHealthBar.value = (float)battleHealthBar.maxValue;



        battleStaminaBar.maxValue = (float)stamina.maxValue;

        battleStaminaBar.onValueChanged.AddListener(
        delegate {
            BattleScript.instance.updateResourceBarText("Battle UC " + id + " StaminaBar");
        });


        battleHead = GameObject.Find("Battle UC " + this.id + " HeadType").GetComponent<Image>();

        


        isUserControllable = true;


    }
}
