using UnityEngine;
using System.Collections;

public class BackgroundClick : MonoBehaviour
{
    public bool acceptInput;

    // Use this for initialization
    void Start()
    {
        acceptInput = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void doNothingClick()
    {
        if (BattleScript.instance.pipeInputFunc != null && acceptInput)
        {
            BattleScript.instance.pipeInputFunc("Background 0");
            return;
        }
    }
}
