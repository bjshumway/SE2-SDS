using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeathControl : MonoBehaviour {
    public Slider PlayerHP;
	// Use this for initialization
	void Start () {
        PlayerHP.maxValue = Combat.playerHP;
        PlayerHP.value = Combat.playerHP;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerHP.value = Combat.playerHP;
	}
}
