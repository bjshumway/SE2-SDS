using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BowAttack : Ability
{

    public GameObject BowAttackSlider = GameObject.Find("BowAttackSlider");

    public GameObject BowAttackSlider2 = GameObject.Find("BowAttackSlider2");

    public GameObject BowAttackSliderBackground = GameObject.Find("BowAttackSliderBackground");
    
    public Actor attackee;
    public Actor attackee2;

    private System.Random rand;
    int numBowAttacks = 0;


    public BowAttack(Actor Owner) : base("Bow Attack", "Dexterity based damage. Damage is multiplied by how well you hit a target that quickly flies across the screen. Hitting the bullsye does 2x damage", 100, false, Owner)
    {
        BowAttackSlider.SetActive(false);
        BowAttackSlider2.SetActive(false);
        BowAttackSliderBackground.SetActive(false);


    }


public void showAnimation(Monster m)
    {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }


    //arg is a string of the format "typeOfThingClickedOn index"
    //e.g. "Monster 1" or "UserControllable 2" or "AbilityBar 1"
    public void selectEnemy(string arg)
    {
        //   check to see what was clicked on is a monster
        //   if it's not a monster do nothing
        string[] args = arg.Split();

        if (args[0] != "Monster" && args[0] != "UserControllable")
        {
            return;
        }
        else
        {
            BattleScript bs = BattleScript.instance;
            Actor a;
            if(args[0] == "UserControllable")
            {
                a = GameMaster.instance.thePlayer.theParty[int.Parse(args[1]) - 1];
            } else
            {
                a = bs.monsters[int.Parse(args[1]) - 1];
            }
            attackee = a;
            startAiming(a.stats["cunning"].effectiveLevel);
        }
    }

    public override void cast(Actor act = null)
    {
        BattleScript bs = BattleScript.instance;

        owner.stamina.setValue(owner.stamina.value - stamina);
        if(owner.hasPassive("DoubleShot"))
        {
            //Get two alive monsters at random
            ArrayList monsters = new ArrayList();
            for (int i = 0; i < bs.monsters.Length; i++)
            {
                if (bs.monsters[i].isAlive) {
                    monsters.Add(bs.monsters[i]);
                }
            }
            rand = new System.Random(this.GetHashCode());
            int k = rand.Next(0, monsters.Count);
            attackee = (Monster) monsters[k];
            if (monsters.Count > 1)
            {
                monsters.Remove(attackee);
            }
            rand = new System.Random(monsters.GetHashCode());
            int k2 = rand.Next(0, monsters.Count);
            attackee2 = (Monster)monsters[k2];
            startAiming(attackee.stats["cunning"].effectiveLevel,
                        attackee2.stats["cunning"].effectiveLevel);

        }
        else if (bs.monsters.Length > 1)
        {
            int aliveCount = 0;
            Monster aliveMonster = null;
            for (int i = 0; i < bs.monsters.Length; i++)
            {
                if (bs.monsters[i].isAlive)
                {
                    aliveCount++;
                    aliveMonster = bs.monsters[i];
                }
            }
            if (aliveCount > 1)
            {
                bs.pipeInputFunc = this.selectEnemy;

                //Tell the user to select a target
                BattleHints.text = MLH.tr("Select Target");
                return;
            }
            else
            {
                attackee = aliveMonster;
                startAiming(aliveMonster.stats["cunning"].effectiveLevel);
            }
        }
        else
        {
            attackee = bs.monsters[0];
            startAiming(bs.monsters[0].stats["cunning"].effectiveLevel);
        }
    }

    //Sets up the aiming slider for the rogue
    //if the second difficultyToHit is set, then assume we are using double-shot
    public void startAiming(int difficultyToHit, int difficultyToHit2 = -1)
    {
        if (difficultyToHit2 == -1)
        {
            BowAttackSlider.GetComponent<Slider>().value = 0;
            BowSliderMove sm = BowAttackSlider.GetComponent<BowSliderMove>();
            sm.isActive = true;
            sm.sliderSpeed = difficultyToHit;
            BattleHints.text = MLH.tr("Press Q When The Target Reaches The Center");
            BowAttackSlider.SetActive(true);
            BowAttackSliderBackground.SetActive(true);
            BattleScript.instance.pipeInputFunc = handleAimInput;
            BattleScript.instance.isPaused = true;
        }
        else
        {
            BowAttackSlider.GetComponent<Slider>().value = 0;
            BowAttackSlider2.GetComponent<Slider>().value = 0;
            BowSliderMove sm = BowAttackSlider.GetComponent<BowSliderMove>();
            sm.isActive = true;
            sm.sliderSpeed = difficultyToHit;
            sm.launchSecondSlider = true;
            sm.secondSliderSpeed = difficultyToHit2;
            sm.secondSliderStartPoint = (decimal) UnityEngine.Random.Range(10,40);
            BattleHints.text = MLH.tr("Press Q When The Target Reaches The Center");

            BowAttackSlider.SetActive(true);
            BowAttackSliderBackground.SetActive(true);
            BattleScript.instance.pipeInputFunc = handleAimInput;
            BattleScript.instance.isPaused = true;
        }
    }

    //Handles aiming to hit the slider
    public void handleAimInput(string args)
    {
        string[] s = args.Split();
        if (s[0] == "Keypress" && s[1] == "q" || (s[0] == "SliderMiss" && s[1] == "0") )
        {
            Debug.Log("handleAimInput, Attackee: " + attackee.name);

            float val;
            GameObject slider = null;
            Actor whichAttackee = null;

            if (!owner.hasPassive("DoubleShot") || numBowAttacks == 0)
            {
                slider = BowAttackSlider;
                whichAttackee = attackee;
            }
            else if(owner.hasPassive("DoubleShot") && numBowAttacks == 1)
            {
                slider = BowAttackSlider2;
                whichAttackee = attackee2;
            }

            //Don't take any input until the second slider is launched
            if (owner.hasPassive("DoubleShot") && numBowAttacks == 0 && 
               !slider.GetComponent<BowSliderMove>().hasLaunchedSecondSlider)
            {
                return;
            }


            val = slider.GetComponent<Slider>().value;


            decimal modifier;
            if (val < 44 || val > 56 )
            {
                //Deal damage equal to percent distance of closeness to target area
                modifier = (decimal) (val < 44 ? val / 44 : ((100 - val)) / 44);
                whichAttackee.damage((decimal)owner.stats["dexterity"].effectiveLevel * owner.weapon.damage * modifier, owner, damageType.ranged);
            }
            else
            {
                modifier = ((owner.hasPassive("Sharp Shooter")) ? 2 : 1);
                whichAttackee.damage(owner.stats["dexterity"].effectiveLevel * owner.weapon.damage * modifier, owner, damageType.ranged, true);
            }

            if (!owner.hasPassive("DoubleShot"))
            {
                BattleScript.instance.pipeInputFunc = null;
                BattleScript.instance.isPaused = false;
                slider.SetActive(false);
                BowAttackSliderBackground.SetActive(false);
                slider.GetComponent<BowSliderMove>().isActive = false;
            } else
            {
                slider.SetActive(false);
                slider.GetComponent<BowSliderMove>().isActive = false;

                if (numBowAttacks == 0)
                {
                    numBowAttacks++;
                    if (!whichAttackee.isAlive)
                    {
                        if(attackee == attackee2)
                        {
                            BattleScript.instance.pipeInputFunc = null;
                            BattleScript.instance.isPaused = false;
                            BowAttackSliderBackground.SetActive(false);
                            BowAttackSlider.GetComponent<BowSliderMove>().hasLaunchedSecondSlider = false;
                            BowAttackSlider2.SetActive(false);
                            BowAttackSlider2.GetComponent<BowSliderMove>().isActive = false;
                            numBowAttacks = 0;
                        }
                        return;
                    }
                }
                else
                {
                    BattleScript.instance.pipeInputFunc = null;
                    BattleScript.instance.isPaused = false;
                    BowAttackSliderBackground.SetActive(false);
                    BowAttackSlider.GetComponent<BowSliderMove>().hasLaunchedSecondSlider = false;
                    numBowAttacks = 0;
                }
                
            }
        }
    }

    public override void onLoad()
    {

    }

    public override void onUnload()
    {
        BowAttackSlider.SetActive(false);
        BowAttackSlider2.SetActive(false);
     }

}