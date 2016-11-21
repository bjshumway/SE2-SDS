using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class ResourceChange : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name =="Resources")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.resourceTotal);
        }
        else if(gameObject.name == "Stamina")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.stamina);
        }
        else if(gameObject.name == "Health")
        {
            GetComponent<Text>().text = Convert.ToString(SkillSelectionScript.health);
        }

    }
}
