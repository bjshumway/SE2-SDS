using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HughShift : MonoBehaviour {

    public static int direction = 1;
    public float time = 3.0f;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.GetComponent<Image>().color = gameObject.GetComponent<Image>().color.hu

        HSBColor hsb = new HSBColor(gameObject.GetComponent<Image>().color);

        if (hsb.h > 128)
        {
            direction = -1;
        }else if(hsb.h < 2)
        {
            direction = 1;
        }

        hsb.h = (hsb.h + Time.deltaTime / time * direction) % 1.0f;

        gameObject.GetComponent<Image>().color = hsb.ToColor();
    }
}
