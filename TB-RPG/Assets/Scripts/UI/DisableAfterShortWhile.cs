using UnityEngine;
using System.Collections;

public class DisableAfterShortWhile : MonoBehaviour {

    public static float timeLeft;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (timeLeft > 0)
        {
            timeLeft = timeLeft - Time.deltaTime;
            if(timeLeft < 0)
            {
                gameObject.SetActive(false);
            }
        } 
    }

    public void showParentTemporarily(float secs)
    {
        timeLeft = secs;
        gameObject.SetActive(true);
    }
}
