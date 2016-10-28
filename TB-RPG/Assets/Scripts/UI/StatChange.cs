using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class StatChange : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.name == "Stats")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.statTotal);
        }

        if (gameObject.name == "Strength")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.strength);
        }

        if (gameObject.name == "Intellect")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.intellect);
        }

        if (gameObject.name == "Dexterity")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.dexterity);
        }

        if (gameObject.name == "Cunning")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.cunning);
        }

        if (gameObject.name == "Charisma")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.charisma);
        }

    }
}
