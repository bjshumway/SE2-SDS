using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Player : UserControllable {

    static Type[] types = new Type[] {
        typeof(Actor),
        typeof(Player),
        typeof(Attack),
        typeof(Ability),
        typeof(SingleTargetAbility),
        typeof(ChargeStrength),
        typeof(ArcaneDestruction),
        typeof(BowAttack),
        typeof(Parry),
        typeof(DoubleShot),
        typeof(Flee),
        typeof(HandyMan),
        typeof(Heal),
        typeof(IronSkin),
        typeof(LastChance),
        typeof(Pin),
        typeof(Poison),
        typeof(Regen),
        typeof(SharpShooter),
        typeof(SwordFlurry),
        typeof(Wither),
        typeof(MeleeWeapon),
        typeof(MagicWeapon),
        typeof(RangedWeapon),
        typeof(Weapon),
        typeof(Item),
        typeof(Inventory),
        typeof(AbilityBar),
        typeof(Resource),
        typeof(Stat),
        typeof(Tier)
    };

    public decimal gold;
    [XmlIgnore]
    public UserControllable[] theParty;

    public Follower[] followers;
    public bool partyIsDead;

    public Inventory inventory;

    public int numBattlesFought;

    public Tier tier;

    public bool beatTheGame;

    public Player() : base()
    {
        this.followers = new Follower[2];
        this.theParty = new UserControllable[3];
        this.theParty[0] = this;
        this.partyIsDead = false;
        this.remainingStatPoints = initialStatPoints;
        this.remainingResourcePoints = 1;
        this.remainingAbilityPoints = 1;
        this.numBattlesFought = 0;
        this.mustBeToldOfNewAbilityPointToSpend = true;

        this.name = "Trifaldo";
        //Debug.Log("stamina: " + this.stamina.maxValue + ", " + this.stamina.value);

        this.inventory = new Inventory(this, "Inventory", 150);
        this.inventory.addItem(new Item("TBD - Not Yet Implemented\r\n(you can buy/sell me though)", 0, false, Item.itemTypes.abilityItem, 0, ""));

    }

    //Adds a party member to the party!
    public void addPartyMember()
    {
        Follower newMember = new Follower();

        followers[newMember.id - 2] = newMember;
        theParty[newMember.id - 1] = newMember;

        CharacterCreationMenu.load(newMember, false);
    }

    /// <summary>
    /// Serializes the UserControllable into the specified binary file
    /// </summary>
    /// <param name="fileName">File to save to</param>
    public void save(string fileName)
    {

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


        using (var writer = File.Create(fileName))
        {
            var serializer = new XmlSerializer(typeof(Player), types);

            // write all of the UC's attribute values to a file in binary format
            serializer.Serialize(writer, this);
        }
    }

    /// <summary>
    /// Creates a UserControllable based on a binary file
    /// </summary>
    /// <param name="fileName">File to read from</param>
    /// <returns>a UserControllable if the file can be parsed, otherwise null</returns>
    public static Player load(string fileName)
    {
        if (File.Exists(fileName))
        {
            try
            {
                using (var writer = File.Open(fileName, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Player), types);
                    return (Player)serializer.Deserialize(writer);
                }

            }
            catch (Exception ex)
            {
                Debug.Log("In UserControllable.load: " + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        return null;
    }
}
