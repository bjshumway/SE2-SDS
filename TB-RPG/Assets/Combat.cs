using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
    public GameObject DemonSkull;

    //We'll use this variable to keep track of whose turn it is
    public static bool PlayerTurn = true;

    public static int playerHP = 200; 
    public static int playerAttack = 25;

    public static int enemyHP = 150;
    public static int enemyAttack = 10;

    public static int potion = 50;

    //Using KeyCode lets you map an action to a key on the keyboard
    public KeyCode attack;
    public KeyCode item;  


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        //activates when player releases key assigned to variable attack
        if (Input.GetKeyUp(attack) && Combat.PlayerTurn == true)
        {
            Combat.enemyHP -= playerAttack;

            //keeps enemy health from going below zero
            if (Combat.enemyHP < 0)
                enemyHP = 0;

            Debug.Log("EnemyHP: " + enemyHP);
            Combat.PlayerTurn = false;
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
