using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public void tryAgain()
    {
        //Todo: Delete all of the necessary classes and reset variables 
        //      so that clicking "start game" starts from scratch.
        GameMaster.instance.switchCamera(0);
    }
}
