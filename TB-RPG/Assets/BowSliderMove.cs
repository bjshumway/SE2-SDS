using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BowSliderMove : MonoBehaviour {

    public decimal sliderSpeed;
    public bool isActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            Slider s = gameObject.GetComponent<Slider>();
            s.value += Time.deltaTime * (float)sliderSpeed * 10;
            if(s.value >= 100)
            {
                isActive = false;
                if(BattleScript.instance.pipeInputFunc != null)
                {
                    BattleScript.instance.pipeInputFunc("SliderMiss 0");
                }
            }
        }

	}
}
