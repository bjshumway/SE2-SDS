using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
    public GameObject DemonSkull;

    //We'll use this variable to keep track of whose turn it is
    public static bool PlayerTurn = true;
    //Battle will end when this variable is true
    public static bool Battle = false;

    public static int playerHP = 200; 
    public static int playerAttack = 25;

    public static int enemyHP = 150;
    public static int enemyAttack = 10;

    public static int potion = 50;

    //Using KeyCode lets you map an action to a key on the keyboard
    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;
    public KeyCode item;  


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        //activates when player releases key assigned to variable attack
        if (Input.GetKeyUp(ability1) && Combat.PlayerTurn == true)
        {
            playerAttack = TestSlash.attack;
            Combat.enemyHP -= playerAttack;

            //keeps enemy health from going below zero
            if (Combat.enemyHP < 0)
                enemyHP = 0;

            Debug.Log("EnemyHP: " + enemyHP);
            if (enemyHP > 0)
                Combat.PlayerTurn = false;
            else
            {
                Destroy(DemonSkull);
                Battle = true;
            }    
        }

        if (Input.GetKeyUp(ability2) && Combat.PlayerTurn == true)
        {
            playerAttack = testSlam.attack;
            Combat.enemyHP -= playerAttack;

            //keeps enemy health from going below zero
            if (Combat.enemyHP < 0)
                enemyHP = 0;

            Debug.Log("EnemyHP: " + enemyHP);
            if (enemyHP > 0)
                Combat.PlayerTurn = false;
            else
            {
                Destroy(DemonSkull);
                Battle = true;
            }
        }

        if (Input.GetKeyUp(ability3) && Combat.PlayerTurn == true)
        {
            playerAttack = testAttack.attack;
            Combat.enemyHP -= playerAttack;

            //keeps enemy health from going below zero
            if (Combat.enemyHP < 0)
                enemyHP = 0;

            Debug.Log("EnemyHP: " + enemyHP);
            if (enemyHP > 0)
                Combat.PlayerTurn = false;
            else
            {
                Destroy(DemonSkull);
                Battle = true;
            }
        }
        //activates when player releases key assigned to variable item
        if (Input.GetKeyUp(item) & Combat.PlayerTurn == true)
        {
            Combat.playerHP += potion;

            //makes sure player cannot go past full health
            if (Combat.playerHP > 200)
                playerHP = 200;

            Debug.Log("Player health: " + playerHP);

            Combat.PlayerTurn = false;
        }
        
        //enemies turn
        if (PlayerTurn == false)
        {
            if (enemyHP < 50)
                enemyAttack = 30;
            else
                enemyAttack = 10;

            Combat.playerHP -= enemyAttack;
            Combat.PlayerTurn = true;

            Debug.Log("PlayerHP: " + Combat.playerHP);
        }
	}
}
