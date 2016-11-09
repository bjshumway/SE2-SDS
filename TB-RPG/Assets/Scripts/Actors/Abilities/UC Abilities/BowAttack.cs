using UnityEngine;
using UnityEngine.UI;

public class BowAttack : Ability
{

    public GameObject BowAttackSlider = GameObject.Find("BowAttackSlider");
    
    public Actor attackee;

    public BowAttack(Actor Owner) : base("Bow Attack", "Dexterity based damage. Damage is multiplied by how well you hit a target that quickly flies across the screen. Hitting the bullsye does 2x damage", 100, false, Owner)
    {
        BowAttackSlider.SetActive(false);
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

        owner.stamina.setValue(0);

        if (bs.monsters.Length > 1)
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
                BattleHints.text = "Select Target";
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
    public void startAiming(int difficultyToHit)
    {
        BowAttackSlider.GetComponent<Slider>().value = 0;
        BowSliderMove sm = BowAttackSlider.GetComponent<BowSliderMove>();
        sm.isActive = true;
        sm.sliderSpeed = difficultyToHit;
        BattleHints.text = "Press Q When The Target Reaches The Center";
        BowAttackSlider.SetActive(true);
        BattleScript.instance.pipeInputFunc = handleAimInput;
        BattleScript.instance.isPaused = true;

    }

    //Handles aiming to hit the slider
    public void handleAimInput(string args)
    {
        string[] s = args.Split();
        if (s[0] == "Keypress" && s[1] == "q" || (s[0] == "SliderMiss" && s[1] == "0") )
        {
            Debug.Log("handleAimInput, Attackee: " + attackee.name);
            float val = BowAttackSlider.GetComponent<Slider>().value;
            decimal modifier;
            if (val < 44 || val > 56 )
            {
                //Deal damage equal to percent distance of closeness to target area
                modifier = (decimal) (val < 44 ? val / 44 : ((100 - val) * -1) / 44);
                attackee.damage((decimal)owner.stats["dexterity"].effectiveLevel * owner.weapon.damage * modifier, owner, damageType.ranged);
            } else
            {
                modifier = ((owner.hasPassive("Sharp Shooter")) ? 4 : 2);
                attackee.damage(owner.stats["dexterity"].effectiveLevel * owner.weapon.damage * modifier, owner, damageType.ranged, true);
            }


            BattleScript.instance.pipeInputFunc = null;
            BattleScript.instance.isPaused = false;
            BowAttackSlider.SetActive(false);
        }
    }

    public override void onLoad()
    {

    }

    public override void onUnload()
    {
        BowAttackSlider.SetActive(false);
    }


}