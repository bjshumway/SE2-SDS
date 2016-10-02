using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour {
    public Slider EnemyHp;

	// Use this for initialization
	void Start () {
        EnemyHp.maxValue = Combat.enemyHP;
        EnemyHp.value = Combat.enemyHP;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyHp.value = Combat.enemyHP;
	}
}
