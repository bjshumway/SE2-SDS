using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BowSliderMove : MonoBehaviour {

    public decimal sliderSpeed;
    public bool isActive;
    public bool launchSecondSlider;
    public decimal secondSliderStartPoint;
    public decimal secondSliderSpeed;

    public bool hasLaunchedSecondSlider;
    private Slider s;
    public GameObject secondSlider;

    // Use this for initialization
    void Start () {
        isActive = false;
        launchSecondSlider = false;
        hasLaunchedSecondSlider = false;
        s = gameObject.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            s.value += Time.deltaTime * (float)sliderSpeed * 10;

            if(s.value > (float) secondSliderStartPoint && launchSecondSlider)
            {
                hasLaunchedSecondSlider = true;
                launchSecondSlider = false;
                secondSlider.SetActive(true);
                BowSliderMove bsm = secondSlider.GetComponent<BowSliderMove>();
                bsm.isActive = true;
                bsm.sliderSpeed = secondSliderSpeed;
            }

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
