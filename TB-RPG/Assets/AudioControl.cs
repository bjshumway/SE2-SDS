using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public static void playSound(string fileName)
    {
        var aSourceGO = Resources.Load("AudioControllerSoundEffect");
        GameObject aSource = (GameObject)Instantiate(aSourceGO);
        aSource.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/" + fileName);
        aSource.GetComponent<AudioSource>().loop = false;
        aSource.GetComponent<AudioSource>().Play();
        GameObject.Destroy(aSource, 10);
    }

    // Update is called once per frame
    void Update () {

	}
}
