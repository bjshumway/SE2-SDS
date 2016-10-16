using UnityEngine;
using System.Collections;

public class MonsterClick : MonoBehaviour {
    public int id;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        Debug.Log("Mouse down for monster with id " + id);
        BattleScript.instance.monsterClick("Monster " + id);
    }
}
