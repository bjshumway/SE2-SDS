using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuScript : MonoBehaviour
{


    public void LoadScene(string cameraNum)
    {
        CharacterCreationMenu.load(GameMaster.instance.thePlayer, true);
    }

    public void Start()
    {

        
        
    }
}
