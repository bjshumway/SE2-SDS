using UnityEngine;
using System.Collections;

public class AbilitySelectionScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void goToNextScene()
    {
        GameMaster.instance.switchCamera(3);
    }
}
