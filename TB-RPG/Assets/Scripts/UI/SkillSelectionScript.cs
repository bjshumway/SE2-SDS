using UnityEngine;
using System.Collections;

public class SkillSelectionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void goToNextScene()
    {
        GameMaster.instance.switchCamera(4);
    }
}
