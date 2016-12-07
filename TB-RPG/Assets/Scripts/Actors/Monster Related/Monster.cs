using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Actor {

    public GameObject monsterPrefab;
    public static int id_increment = 1;

    public bool isBoss;

    public Ability[] abilities;

    private Item _drop;

    //The difficulty of the monster in the level
    //Ranges from 1 to 4
    //A monster of difficulty 
    public int difficultyInLevel;

    public decimal hitAccuracy;

    public bool isFinalBoss;

    public Item itemDrop {
        get {
            return _drop;
        }
        set
        {
            _drop = value;
        }
    }

    //How much gold is dropped. Defined in the overrided kill function.
    public decimal goldDrop;

    //Whether this monster has been stolen from. False by default.
    public bool stolenFrom;

    public Monster(string name, String prefabName, int level, int diffInLevel, int hitAcc, bool isBoss, Title title = null, Resource[] resources = null, int[] stats = null, Ability.damageType weakness = Ability.damageType.none)
        : base(name, level, title, resources, stats, weakness) {

        this.isBoss = isBoss;
        stolenFrom = false;
        hitAccuracy = hitAcc;
        difficultyInLevel = diffInLevel;
        this.id = id_increment;
        //Debug.Log("Creating Monster with id" + this.id);
        id_increment++;
        

        GameObject imagePrefab = Resources.Load("Monsters/" + prefabName) as GameObject;
        monsterPrefab = GameObject.Instantiate(imagePrefab, imagePrefab.transform.position, imagePrefab.transform.rotation) as GameObject;
        monsterPrefab.transform.SetParent(GameObject.Find("BattleCanvas").transform, false);
        //Debug.Log(monsterPrefab);


        if (!isBoss)
        {
            if (difficultyInLevel == 2)
            {
                monsterPrefab.GetComponent<SpriteRenderer>().color = new Color32(0,0,255,255);
            }
            else if (difficultyInLevel == 3)
            {
                monsterPrefab.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            }
        }


        //Setup the healthbar for this monster
        battleHealthBar = monsterPrefab.transform.FindChild("Monster HealthBar").gameObject.GetComponent<Slider>();
        battleHealthBar.name = "Monster " + id + " HealthBar";
        battleHealthBar.maxValue = (float)health.maxValue;
        battleHealthBar.value = battleHealthBar.maxValue;
        health.sliders = new Slider[] { battleHealthBar };


        //Setupe the staminabar for this monster
        battleStaminaBar = monsterPrefab.transform.FindChild("Monster StaminaBar").gameObject.GetComponent<Slider>();
        battleStaminaBar.name = "Monster " + id + " StaminaBar";
        battleStaminaBar.maxValue = (float)stamina.maxValue;
        stamina.sliders = new Slider[] { battleStaminaBar };

        //Setup the battleDamageText for this monster
        battleDamageText = monsterPrefab.transform.FindChild("Monster BattleDamageText").gameObject;
        battleDamageText.name = "Monster " + id + " BattleDamageText";

        //Setup the battleStatusEffectText for this monster
        battleStatusEffectText = monsterPrefab.transform.FindChild("Monster StatusEffectText").gameObject;
        battleStatusEffectText.name = "Monster " + id + " StatusEffectText";


        //Set battleDamageText to inactive so that it doesn't show up on the screen.
        //We needed it to be active initially so that we could find it with GameObject.find
        //(inactive gameobjects can't be found that way)
        battleDamageText.SetActive(false);


        //Setup clicking on the monster
        //Requires all monsters to have a script called MonsterClick attached to them
        MonsterClick mc = (MonsterClick)monsterPrefab.GetComponent<MonoBehaviour>();
        mc.id = this.id;
        
        
    }

    //This function is the AI that the monster takes during battle
    //This constantly gets called in the Update function of BattleScript
    //By default it selects at random and tries to cast it.
    //This function can be overridden for any enemy that has a more complicated AI
    public void doBattleAI()
    {
        System.Random ran = new System.Random();
        int choice = ran.Next(abilities.Length-1);
        if(abilities[choice].stamina <= stamina.value)
        {
            abilities[choice].cast();
        }
    }


    //Generates a single random number based on the mapTier
    public static Monster getRandomMonsterByLevel(int mapTier)
    {
        //The number of monster types particular to a specific mapTier
        int numMonsterTypes;

        //The index for the monster that will be selected
        int monsterIndex;

        System.Random randNum = new System.Random();

        switch (mapTier)
        {
            case 1:
                numMonsterTypes = 1;
                monsterIndex = randNum.Next(numMonsterTypes);
                switch (monsterIndex)
                {
                    case 0:
                        return new DemonSkull1_3();
                }
                break;
        }



        System.Console.WriteLine("level out of bounds for function monsterTypesByLevel");
        return null;
    }



    //Randomly creates 1 to 3 Monsters that correspond to the mapLevel
    //Their combined difficultyInLevel should be equal to numFightsInTier
    public static Monster[] genMonstersByLevel(int mapTier, int difficulty)
    {
        //Debug.Log("Inside genMonstersByLevel");
        int sumDifficulty = 0;

        List<Monster> monsters = new List<Monster>();
        string fight = "" + mapTier + "_" + difficulty;
        
        switch(fight)
        {
            case "1_1":
                monsters.Add(new DemonSkull1_1());
                break;
            case "1_2":
                monsters.Add(new DemonSkull1_1());
                monsters.Add(new DemonSkull1_2());
                break;
            case "1_3":
                monsters.Add(new DemonSkull1_1());
                monsters.Add(new DemonSkull1_2());
                monsters.Add(new DemonSkull1_3());
                break;
            case "1_4":
                monsters.Add(new Orca());
                break;
            case "2_1":
                monsters.Add(new DemonSkull2_1());
                break;
            case "2_2":
                monsters.Add(new DemonSkull2_1());
                monsters.Add(new DemonSkull2_2());
                break;
            case "2_3":
                monsters.Add(new DemonSkull2_1());
                monsters.Add(new DemonSkull2_2());
                monsters.Add(new DemonSkull2_3());
                break;
            case "2_4":
                monsters.Add(new DemonSkull2_3());
                monsters.Add(new Reaper());
                break;
            case "3_1":
                monsters.Add(new DemonSkull3_1());
                break;
            case "3_2":
                monsters.Add(new DemonSkull3_1());
                monsters.Add(new DemonSkull3_2());
                break;
            case "3_3":
                monsters.Add(new DemonSkull3_1());
                monsters.Add(new DemonSkull3_2());
                monsters.Add(new DemonSkull3_3());
                break;
            case "3_4":
                monsters.Add(new Bunny());
                monsters.Add(new DemonSkull3_2());
                monsters.Add(new DemonSkull3_3());
                break;
            default:
                //We are here because we fought all the bad guys and won! Now for a challenge....
                Bunny b = new Bunny();
                //Scale Factor
                var scaleFactor = (Math.Pow((1.1), GameMaster.instance.thePlayer.numBattlesFought - 11));
                b.stats["strength"].setLevel((int)b.stats["strength"].level * (int)scaleFactor);
                b.stamina.refreshSpeed = b.stamina.refreshSpeed * (int) scaleFactor;

                var d2 = new DemonSkull3_2();
                d2.stats["strength"].setLevel((int)b.stats["strength"].level * (int)scaleFactor);
                d2.stamina.refreshSpeed = b.stamina.refreshSpeed * (int)scaleFactor;

                var d3 = new DemonSkull3_3();
                d3.stats["strength"].setLevel((int)b.stats["strength"].level * (int)scaleFactor);
                d3.stamina.refreshSpeed = b.stamina.refreshSpeed * (int)scaleFactor;


                monsters.Add(b);
                monsters.Add(d2);
                monsters.Add(d3);


                break;
        }


        return monsters.ToArray();
    }

    public override void kill() {
        base.kill();
        _drop = Gen.drop(level);
        goldDrop = 10 * level * difficultyInLevel;
    }
}