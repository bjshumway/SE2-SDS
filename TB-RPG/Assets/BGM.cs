using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {


    public AudioSource source;

    public AudioClip menuClip;
    public float menuClipTime = 0;

    public AudioClip battleClip;
    public float battleClipTime = 0;

    public AudioClip shopClip;
    public float shopClipTime = 0;

    public AudioClip bossClip;
    public float bossClipTime = 0;

    public AudioClip victoryClip;
    public float victoryClipTime = 0;

    public AudioClip deathClip;
    public float deathClipTime = 0;

    private SongNames currSong;


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

        switch(currSong)
        {
            case SongNames.menu:
                menuClipTime = source.time - .5f;
                break;
            case SongNames.battle:
                battleClipTime = source.time - .5f;
                break;
            case SongNames.boss:
                bossClipTime = source.time - .5f;
                break;
            case SongNames.shop:
                shopClipTime = source.time - .5f;
                break;
            case SongNames.victory:
                victoryClipTime = source.time - .5f;
                break;
            case SongNames.death:
                deathClipTime = source.time - .5f;
                break;
        }

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

        currSong = sn;

        source.clip = toPlay;
        source.Play();

        try
        {
            switch (sn)
            {
                case SongNames.menu:
                    source.time = menuClipTime;
                    break;
                case SongNames.battle:
                    source.time = battleClipTime;
                    break;
                case SongNames.boss:
                    source.time = bossClipTime;
                    break;
                case SongNames.shop:
                    source.time = shopClipTime;
                    break;
                case SongNames.victory:
                    source.time = victoryClipTime;
                    break;
                case SongNames.death:
                    source.time = deathClipTime;
                    break;
            }
        }
        catch
        {
            switch (sn)
            {
                case SongNames.menu:
                    menuClipTime = 0;
                    break;
                case SongNames.battle:
                    battleClipTime = 0;
                    break;
                case SongNames.boss:
                    bossClipTime = 0;
                    break;
                case SongNames.shop:
                    shopClipTime = 0;
                    break;
                case SongNames.victory:
                    victoryClipTime = 0;
                    break;
                case SongNames.death:
                    deathClipTime = 0;
                    break;
            }
            source.time = 0;
        }

      
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
