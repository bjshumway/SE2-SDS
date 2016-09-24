using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    public void LoadScene(string sceneToLoad)
    {
        Camera.
        //Application.LoadLevel(sceneToLoad);
        //SceneManager.LoadScene(sceneToLoad);

    }

    //Loads the scene based on argument
    //No longer useful because we don't switch between scenes anymore
    /*public void LoadScene(string sceneToLoad)
    {
        Debug.Log(sceneToLoad);
        //Application.LoadLevel(sceneToLoad);
        //SceneManager.LoadScene(sceneToLoad);
        
    }*/
}
