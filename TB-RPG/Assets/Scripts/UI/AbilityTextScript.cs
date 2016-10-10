using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityTextScript : MonoBehaviour {
    public Text ability1;
    public Text ability2;
    public Text ability3;
	// Use this for initialization
	void Start () {
        ability1.text = AbilitySelectionScript.ability1;
        ability2.text = AbilitySelectionScript.ability2;
        ability3.text = AbilitySelectionScript.ability3;

    }

    // Update is called once per frame
    void Update () {
        ability1.text = AbilitySelectionScript.ability1;
        ability2.text = AbilitySelectionScript.ability2;
        ability3.text = AbilitySelectionScript.ability3;    }
}
