using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuScript : MonoBehaviour
{


    public void LoadScene(string cameraNum)
    {
        GameMaster.instance.switchCamera(int.Parse(cameraNum));
    }

    public void Start()
    {
        //GameObject ShowMe = (GameObject) Resources.Load("");
        //GameObject myObj2 = Instantiate(ShowMe, transform.position,transform.rotation) as GameObject;
        //Debug.Log(myObj2);
        //myObj2.transform.SetParent(GameObject.Find("TitleMenuCanvas").transform, false);

        /*Text myObj = GameObject.Find("HelloPrefab") as Text;
        myClone = Instantiate(myObj, transform.position, transform.rotation);

        Text germSpawned = Instantiate(HelloPrefab) as GameObject;*/

        //GameObject canvas = GameObject.Find("TitleMenuCanvas");
        // prefabobject.transform.position, prefabobject.transform.rotation
        //(myObj2).transform.SetParent(canvas.transform);
        //germSpawned.transform.localPosition = spawnPosition;
        //germSpawned.transform.localRotation = spawnRotation;

        //Loads the scene based on argument
        //No longer useful because we don't switch between scenes anymore
        /*public void LoadScene(string sceneToLoad)
        {
            Debug.Log(sceneToLoad);
            //Application.LoadLevel(sceneToLoad);
            //SceneManager.LoadScene(sceneToLoad);

        }*/
    }
}
