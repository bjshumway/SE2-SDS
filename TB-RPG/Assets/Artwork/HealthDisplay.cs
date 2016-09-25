using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //renders text
        GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.name == "PlayerHP")
            //gets text mesh component in object and changes text displayed
            GetComponent<TextMesh>().text = "Player HP " + Combat.playerHP;

        if (gameObject.name == "EnemyHP")
            GetComponent<TextMesh>().text = "Player HP " + Combat.enemyHP;

    }
}
