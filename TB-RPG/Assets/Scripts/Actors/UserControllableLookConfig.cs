using UnityEngine;
using System.Collections;

public class UserControllableLookConfig : MonoBehaviour {

    public int[,] colors = new int[4, 4] { {255,255,255,255 },
                                           {75,75,10,255 },
                                           {100,10,10,255 },
                                           {10,100,10,255 },
                                            };
    public Sprite[] heads;


    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static UserControllableLookConfig s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static UserControllableLookConfig instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first UserControllableLookConfig object in the scene.
                s_Instance = FindObjectOfType(typeof(UserControllableLookConfig)) as UserControllableLookConfig;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("UserControllable");
                s_Instance = obj.AddComponent(typeof(UserControllableLookConfig)) as UserControllableLookConfig;
                Debug.Log("Could not locate an GameMaster object. GameMaster was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }
}
