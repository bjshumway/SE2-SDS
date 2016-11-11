using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// TODO: add talent system; don't know how to flesh out talentPoints yet
public abstract class UserControllable : Actor {
    private int _talentPoints;

    //Assigns the id to each UserControllable
    protected static int id_increment = 1;
    public AbilityBar abilities;

    public int remainingStatPoints;
    public int remainingResourcePoints;

    public Sprite _headType;
    public Color32 _headColor;

    public Image battleHead;

    public enum classTypes {
        fighter,
        mage,
        rogue
    };

    public classTypes classType;


    public int talentPoints {
        get {
            return _talentPoints;
        }
    }

    public Sprite headType
    {
        get; set;
    }

    public Color32 headColor
    {
        get; set;
    }

    public UserControllable() : base()
    {

        abilities = new AbilityBar(this);

        this.id = id_increment;
        id_increment++;

        remainingStatPoints = 10;
        remainingResourcePoints = 1;

        learnAbility(new ItemAbility(this));

        GameObject battleObj = GameObject.Find("Battle UC " + id);

        //Setup the battleHealtBar
        GameObject bHB_gameObj = battleObj.transform.FindChild("Battle UC " + id + " HealthBar").gameObject;
        bHB_gameObj.SetActive(true);
        battleHealthBar = bHB_gameObj.GetComponent<Slider>();
        battleHealthBar.maxValue = (float)health.maxValue;
        battleHealthBar.value = (float)battleHealthBar.maxValue;
        health.sliders = new Slider[] { battleHealthBar };

        //Setup the battleStamina Bar
        GameObject bSB_gameObj = battleObj.transform.FindChild("Battle UC " + id + " StaminaBar").gameObject;
        bSB_gameObj.SetActive(true);
        battleStaminaBar = bSB_gameObj.GetComponent<Slider>();
        battleStaminaBar.maxValue = (float)stamina.maxValue;
        stamina.sliders = new Slider[] { battleStaminaBar };

        //Setup statusEffectText
        battleStatusEffectText = battleObj.transform.FindChild("Battle UC " + id + " StatusEffectText").gameObject;
        battleStatusEffectText.SetActive(true);

        //Setup the background for statusEffectText
        battleStatusEffectBackground = battleObj.transform.FindChild("Battle UC " + id + " StatusEffectText").gameObject;
        battleStatusEffectBackground.SetActive(true);

        //Setup damageText
        battleDamageText = battleObj.transform.FindChild("Battle UC " + id + " BattleDamage").gameObject;
        //battleDamageText.SetActive(true);



        battleHead = GameObject.Find("Battle UC " + this.id + " HeadType").GetComponent<Image>();

        remainingStatPoints = 10;
        remainingResourcePoints = 1;



        isUserControllable = true;
    }


    //This function learns a given ability
    //Sets the owner to this, and isLearned to true
    //Note that UserControllable abilities are already auto-created
    public void learnAbility(Ability ab)
    {
        ab.owner = this;
        ab.isLearned = true;

        if(ab.name == "ITEM")
        {
            abilities.itemAbility = ab;
        }
        else if (!ab.isPassive)
        {
            Ability[] abs = this.abilities.abilities;
            for (int i = 0; i < abs.Length; i++)
            {
                if(abs[i] == null)
                {
                    abs[i] = ab;
                    break;
                }
            }
        } else
        {
            passiveAbilities.Add(ab);
            if(ab.name == "HandyMan")
            {
                abilities.itemAbility.stamina = 5;
            }
        }
        Text t = ab.learnButton.GetComponentInChildren<Text>();
        t.text= MLH.tr(ab.name) + "\n" + MLH.tr("(LEARNED)");
    }

    //This function learns a given ability
    //Sets the owner to null, and isLearned to false
    //Note that UserControllable abilities are already auto-created
    public void unlearnAbility(Ability ab)
    {
        ab.owner = null;
        ab.isLearned = false;

        if (!ab.isPassive)
        {
            Ability[] abs = this.abilities.abilities;
            for (int i = 0; i < abs.Length; i++)
            {
                if (abs[i] == ab)
                {
                    abs[i] = null;
                }
            }
        }
        else
        {
            passiveAbilities.Remove(ab);
        }

        ab.learnButton.GetComponent<Text>().text = MLH.tr(ab.name);
    }

    //Add 3 to remaining stat points
    //Add 1 to remaining resource points
    public void levelUp()
    {
        remainingStatPoints += 3;
        remainingResourcePoints += 1;
    }

    public override void kill() {
        base.kill();

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
