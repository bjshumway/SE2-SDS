using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SaveScriptsOnStartup
{
    static SaveScriptsOnStartup()
    {
        EditorApplication.playmodeStateChanged = () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                Debug.Log("Auto-Saving scene before entering Play mode: " + EditorApplication.currentScene);

                EditorApplication.SaveScene();
                EditorApplication.SaveAssets();
            }
        };
    }
}