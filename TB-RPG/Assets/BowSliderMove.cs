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
            gameObject.GetComponent<Slider>().value += Time.deltaTime * (float)sliderSpeed * 10;
        }
	}
}
