using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;

public class GameMaster : MonoBehaviour {

    public Camera[] theCameras;
    public Canvas[] theCanvases;


    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameMaster s_Instance = null;

    public Player thePlayer;
    public Texture2D cursor1;
    public Texture2D cursor2;

    //How many battles early boss fights become available
    public int minFightsBeforeBoss;

    //Avgerage number of seconds each fight takes, across the game
    public int avgSecPerFight;
    
    //How long it will take for the user to beat the fight
    public int avgMinsToWinGame;

    //How many minutes the user is expected to be playing outside of combat
    public int avgMinNotFighting;

    //Expected timings for what level the user will be at
    //The first argument is the leve, the second argument is the minutes played so far
    public double[,] expectedLevelingTimings;

    //Slowest the stamina can grow per second
    public int slowestStaminaPerSec;

    //Highest stamina by level 15, assume the user went for only stamina, not increasing HP
    public int highestStaminaByLv15;


    public int expGainedStart;
    public int expGainedGrowth;

    public string language;


    // Use this for initialization
    void Start() {
        initGameDesignParameters();

        #region Translation

        if (language != "english") {
            Debug.Log(MLH.populateDict(language));

            Text[] labels = FindObjectsOfType<Text>();
            Dropdown[] dropdowns = FindObjectsOfType<Dropdown>();

            //List<string> lst = new List<string>();

            foreach (Text label in labels)
            {
                string text = MLH.tr(label.text);
                label.text = text;
                //lst.Add(text);
            }

            foreach (Dropdown dropdown in dropdowns) {
                foreach (Dropdown.OptionData option in dropdown.options) {
                    string text = MLH.tr(option.text);
                    option.text = text;
                    //lst.Add(text);
                }
            }

        }
        GameObject.Find("Victory").SetActive(false);

        #endregion

        thePlayer = new Player();
        thePlayer.health.setValue(10);
        


        initGameDesignParameters();




        //setup the mice
        cursor1 = (Texture2D)Resources.Load("mouse1");
        cursor2 = (Texture2D)Resources.Load("mouse2");
        Cursor.SetCursor(cursor1, new Vector2(0, 0), CursorMode.Auto);

        //Set the background by tier
        switchBackground(3);

        //Debug.Log("thePlayer Created");
        //disable all cameras but the one one at 0
        for (int i = 1; i < theCameras.Length; i++)
        {
            theCameras[i].enabled = false;
            theCanvases[i].enabled = false;
        }

        //thePlayer.save("test.save");
    }

    void initGameDesignParameters()
    {
        /*
        int timeSpentInBattle = avgMinsToWinGame - avgMinNotFighting;

        //modulate based on average time to win
        double mod = avgMinsToWinGame / 67;
        expectedLevelingTimings = 
            new double[,] {
                { 2,  1 * mod},
                { 3,  2 * mod},
                { 4,  3 * mod},
                { 5,  5.5 * mod}, //added 2.5
                { 6,  8.5 * mod}, //added 3
                { 7,  12 * mod}, //3.5
                { 8,  16 * mod}, //4
                { 9,  20.5 * mod}, //4.5
                { 10, 25.5 * mod}, //5
                { 11, 31 * mod}, //5.5
                { 12, 39 * mod},
                { 13, 47 * mod },
                { 14, 56 * mod},
                { 15, 67 * mod }
            };

        double[,] battlesFoughtSoFar = new double[15,1];
        for(int i = 0; i < expectedLevelingTimings.Length; i++)
        {
            double minutesPlayed = expectedLevelingTimings[i, 1];
            double minsOfBattleAtLevel = minutesPlayed * ((avgMinNotFighting - avgMinsToWinGame) / avgMinsToWinGame);
            battlesFoughtSoFar[i, 0] = i;
            battlesFoughtSoFar[i, 1] = Mathf.RoundToInt((float) (minsOfBattleAtLevel / (avgSecPerFight * 60)));
        }


        int[,] battlesToNextLevelUp = new double[15, 1];
        for (int i = 0; i < expectedLevelingTimings.Length -1; i++)
        {
            battlesToNextLevelUp[i,1] = Mathf.RoundToInt((float)(battlesFoughtSoFar[i, 1] - battlesFoughtSoFar[i+1, 1]));
        }


        int[,] xpToNextLevel = new int[15, 1];
        int currXp = expGainedStart;
        for (int i = 0; i < xpToNextLevel.Length; i++)
        {
            xpToNextLevel = 
            expectedLevelingTimings //how many battles to fight 
        }
        */

    }

    // Update is called once per frame
    void Update () {

        //f key to toggle full-screen
        if (Input.GetKeyDown(KeyCode.F))
            Screen.fullScreen = !Screen.fullScreen;
    }

    //Switches the background on all camnvases, based on tier.
    public void switchBackground(int tier) {
        string nameOfBackground = null;
        switch(tier)
        {
            case 1: nameOfBackground = "forest"; //TODO: get actual name
                break;
            case 2: nameOfBackground = "cave background"; 
                break;
            case 3: nameOfBackground = "graveyard";
                break;
        }

        //Debug.Log(tier);
        Sprite spr = Resources.Load<Sprite>(nameOfBackground);



        GameObject[] gos = GameObject.FindGameObjectsWithTag("GameBackground");
        for(int i = 0; i < gos.Length;i++)
        {
            //Debug.Log(gos[i]);
            gos[i].GetComponent<Image>().sprite = spr;
        }

   }


    //Switches to the new camera
    public void switchCamera(int camNum)
    {
        for (int i = 0; i < theCameras.Length; i++)
        {
            theCameras[i].enabled = false;
            theCanvases[i].enabled = false;
        }
        theCameras[camNum].enabled = true;
        theCanvases[camNum].enabled = true;
    }

    public void initBattle()
    {
        //Set the character portraits

    }


    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static GameMaster instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first GameMaster object in the scene.
                s_Instance = FindObjectOfType(typeof(GameMaster)) as GameMaster;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GameMaster");
                s_Instance = obj.AddComponent(typeof(GameMaster)) as GameMaster;
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
