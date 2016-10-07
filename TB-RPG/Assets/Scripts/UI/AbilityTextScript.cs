using UnityEngine;
using System.Collections;

public class AbilityTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name == "abl1")
            //gets text mesh component in object and changes text displayed
            GetComponent<TextMesh>().text = AbilitySelectionScript.ability1;

        if (gameObject.name == "abl2")
            GetComponent<TextMesh>().text = AbilitySelectionScript.ability2;

        if (gameObject.name == "abl3")
            GetComponent<TextMesh>().text = AbilitySelectionScript.ability3;
    }
}
