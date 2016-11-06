using UnityEngine;
using System.Collections.Generic;

public class DamageFloatUpward : MonoBehaviour {

    public float curTime;
    public float floatingTime = 2;
    public float timeOfCreation;
    public bool isCopy = false;

    //This class handles the behavior for damageText

    void Update () {
        //Only copies are allowed to have this behavior
        //This is because we are copying from a damageText gameObject that isn't meant to do anything but have copies created of it
        //Hence the check for isCopy
        if (isCopy)
        {
            curTime = Time.realtimeSinceStartup;
            Vector2 newPos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector2(newPos.x, newPos.y + Time.smoothDeltaTime*15);
            if (curTime > timeOfCreation + 2)
            {
                Destroy(gameObject);
            }
        }
    }

    //Causes the text to float upward then disappear
    //Sets isCopy to true so that the game knows we want to do this
    public void floatUpThenDisappear(GameObject dmgText)
    {
        timeOfCreation = Time.realtimeSinceStartup;
        isCopy = true;
    }

}
