using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    //Loads the scene based on argument
    public void LoadScene(string sceneToLoad)
    {
        Debug.Log(sceneToLoad);
        //Application.LoadLevel(sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
