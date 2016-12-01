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
    public AbilityBar abilities;

    public int remainingStatPoints;
    public int remainingResourcePoints;

    public Sprite _headType;
    public Color32 _headColor;

    public Image battleHead;

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
        shopInventoryGameObj = GameObject.Find("ShopInventory UC " + id);
        shopInventoryInfo = shopInventoryGameObj.transform.FindChild("Information").gameObject;
        shopInventoryInfo.SetActive(true);

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
                //Item was too heavy to add to inventory - can't equip
                Debug.Log("Error Equipping Item - too heavy");
                return;
            }
        }

        //Do we currently have something equipped? The unequip it.
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
            .GetComponentInChildren<Text>().text = weapon.name;

        return;
    }

    //Add 3 to remaining stat points
    //Add 1 to remaining resource points
    public void levelUp()
    {
        remainingStatPoints += 3;
        remainingResourcePoints += 1;
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

    /// <summary>
    /// Serializes the UserControllable into the specified binary file
    /// </summary>
    /// <param name="fileName">File to save to</param>
    public void save(string fileName) {

        foreach (var uc in GameMaster.instance.thePlayer.theParty)
        {
            if (uc != null)
            {
                uc.statsArray[0] = uc.stats["strength"];
                uc.statsArray[1] = uc.stats["intellect"];
                uc.statsArray[2] = uc.stats["charisma"];
                uc.statsArray[3] = uc.stats["dexterity"];
                uc.statsArray[4] = uc.stats["cunning"];
            }
        }


        using (var writer = File.Create(fileName)) {
            var serializer = new XmlSerializer(typeof(UserControllable));

            // write all of the UC's attribute values to a file in binary format
            serializer.Serialize(writer, this);
        }
    }

    /// <summary>
    /// Creates a UserControllable based on a binary file
    /// </summary>
    /// <param name="fileName">File to read from</param>
    /// <returns>a UserControllable if the file can be parsed, otherwise null</returns>
    public static UserControllable load(string fileName) {
        if (File.Exists(fileName)) {
            try {
                using (var writer = File.Open(fileName, FileMode.Open)) {
                    var serializer = new XmlSerializer(typeof(UserControllable));
                    return (UserControllable)serializer.Deserialize(writer);
                }

            } catch (Exception ex) {
                Debug.Log("In UserControllable.load: " + ex.Message);
            }
        }

        return null;
    }
}
