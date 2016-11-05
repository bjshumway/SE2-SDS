using UnityEngine;
using System.Collections;

public class DamageFloatUpward : MonoBehaviour {

    public static float timeLeft;
    public static float floatingTime = 2;
    public static string myName;


    // Use this for initialization
    void Start () {

    }


    void Update () {
        if (timeLeft > 0)
        {
            timeLeft = timeLeft - Time.deltaTime;
            if (timeLeft < 0)
            {
                Debug.Log("deleting " + name);


                DestroyImmediate(GameObject.Find(myName));
            }
        }
    }

    //Causes the text to float upward then disappear
    public void floatUpThenDisappear(string n)
    {
        timeLeft = floatingTime;
        gameObject.SetActive(true);
        myName = n;
    }

}
