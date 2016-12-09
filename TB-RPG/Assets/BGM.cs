using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {


    public AudioSource source;

    public AudioClip menuClip;
    public AudioClip battleClip;
    public AudioClip shopClip;
    public AudioClip bossClip;
    public AudioClip victoryClip;
    public AudioClip deathClip;


    public enum SongNames
    {
        menu,
        battle,
        boss,
        shop,
        death,
        victory
    }

    private static BGM s_Instance = null;

    // Use this for initialization
    void Start () {
	
    }
	
    // Update is called once per frame
    void Update () {
	
    }

    public void setMusic(SongNames sn)
    {
        AudioClip toPlay = null;

        switch (sn)
        {
            case SongNames.menu:
                toPlay = menuClip;
                break;
            case SongNames.battle:
                toPlay = battleClip;
                break;
            case SongNames.boss:
                toPlay = bossClip;
                break;
            case SongNames.shop:
                toPlay = shopClip;
                break;
            case SongNames.victory:
                toPlay = victoryClip;
                break;
            case SongNames.death:
                toPlay = deathClip;
                break;
        }

        source.clip = toPlay;
        source.Play();

    }

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static BGM instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first BGM object in the scene.
                s_Instance = FindObjectOfType(typeof(BGM)) as BGM;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("BattleScript");
                s_Instance = obj.AddComponent(typeof(BGM)) as BGM;
                //Debug.Log("Could not locate an BGM object. BGM was Generated Automaticly.");
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
