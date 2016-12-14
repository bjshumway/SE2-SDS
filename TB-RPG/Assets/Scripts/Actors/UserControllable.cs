using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class UserControllable : Actor {
    private int _talentPoints;

    //Assigns the id to each UserControllable
    protected static int id_increment = 1;
    protected static int statPointsPerLevel = 3;
    protected static int initialStatPoints = 10;


    public AbilityBar abilities;

    public bool mustBeToldOfNewAbilityPointToSpend;



    private int _remainingStatPoints;
    public int remainingStatPoints
    {
        set
        {
            this._remainingStatPoints = value;
        }
        get
        {
            return _remainingStatPoints;
        }
    }

    public int remainingResourcePoints;

    [XmlIgnore]
    public Sprite _headType;
    public Color32 _headColor;

    [XmlIgnore]
    public Image battleHead;
    public GameObject battleObj;

    [XmlIgnore]
    public GameObject shopInventoryGameObj;

    [XmlIgnore]
    public GameObject shopInventoryInfo;

    public enum classTypes {
        fighter,
        mage,
        rogue
    };

    public classTypes classType;
    internal int remainingAbilityPoints;

    public int talentPoints {
        get {
            return _talentPoints;
        }
    }

    [XmlIgnore]
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


        level = 1;

        /* Deprecated - disabled learning the item ability. We may bring it back in a later instance of the game.
            learnAbility(new ItemAbility(this));
        */


        battleObj = GameObject.Find("Battle UC " + id);
        shopInventoryGameObj = GameObject.Find("ShopInventory UC " + id);
        shopInventoryInfo = shopInventoryGameObj.transform.FindChild("Information").gameObject;
        shopInventoryInfo.SetActive(true);
        shopInventoryGameObj.transform.FindChild("HeadType").gameObject.SetActive(true);

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

    //Equip
    public void equipWeapon(Weapon wpn)
    {

        //First make sure the weapon is in the inventory, if it isn't add it to the inventory
        bool foundItem = false;
        List<Item> items = GameMaster.instance.thePlayer.inventory.items;
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] == wpn)
            {
                foundItem = true;
                break;
            }
        }

        if(!foundItem)
        {
            bool success = GameMaster.instance.thePlayer.inventory.addItem(wpn);
            if(!success)
            {
                //Cheat! Make the inventory big enough to equip it since otherwise we get a bug involving
                //a newly added userControllable not having a weapon.
                //Thankfully, this is the only case where we have to cheat, and we only add a weapon of weight 1
                GameMaster.instance.thePlayer.inventory.weightCap += 1;
                GameMaster.instance.thePlayer.inventory.addItem(wpn);
                return;
            }
        }

        //Do we currently have something equipped? Then unequip it.
        if (weapon != null)
        {
            weapon.invObject.SetActive(true);
            weapon.isEquipped = false;
        }
        //Now that it's in the inventory, we want to equip it..
        //so remove it from the scrollview simply by disabling it
        weapon = wpn;

        weapon.invObject.SetActive(false);
        weapon.isEquipped = true;


        //Next set the name of our current equip to what we are equipping
        shopInventoryInfo.transform.FindChild("Weapon").gameObject
            .GetComponentInChildren<Text>().text = weapon.name + ", dmg: " + weapon.level;

        return;
    }

    //Add 3 to remaining stat points
    //Add 1 to remaining resource points
    //Add 1 to remaining ability points, but only if we haven't gained a party member
    public void levelUp()
    { 
        remainingStatPoints += statPointsPerLevel;
        remainingResourcePoints += 1;

        if((level + 1) % 3 == 0)
        {
            remainingAbilityPoints += 1;
            mustBeToldOfNewAbilityPointToSpend = true;
        }
        level += 1;
    }

    //gets the number of alive party members
    public static List<UserControllable> getAliveMembers()
    {
        List<UserControllable> mems = new List<UserControllable>();
        for (int i = 0; i < GameMaster.instance.thePlayer.theParty.Length; i++)
        {
            if(GameMaster.instance.thePlayer.theParty[i] != null && GameMaster.instance.thePlayer.theParty[i].isAlive)
            {
                mems.Add(GameMaster.instance.thePlayer.theParty[i]);
            }

        }
        return mems;
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
