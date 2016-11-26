using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public AudioSource source;
    public AudioClip dynamic;

    // Use this for initialization
    void Start () {
        AudioClip ac = (AudioClip) Resources.Load("Audio/ice_spell");
        /*source.PlayOneShot(ac);
        // ac = (AudioClip)Resources.Load("Audio/door_open");

        //source.PlayOneShot(ac);
        ac = (AudioClip)Resources.Load("Audio/fire_spell");
        source.volume = 1;
        source.PlayOneShot(ac);
        */

        var aSourceGO = Resources.Load("AudioControllerSoundEffect");
        GameObject aSource = (GameObject) Instantiate(aSourceGO);
        aSource.GetComponent<AudioControl>().dynamic = (AudioClip)Resources.Load("Audio/door_open");//(ac);
        aSource.GetComponent<AudioControl>().playSound(ac);

    }

    void playSound(AudioClip ac)
    {
        source.PlayOneShot(ac);
    }

    // Update is called once per frame
    void Update () {
	


	}
}
