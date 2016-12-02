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




    //Slowest the stamina can grow per second
    public int slowestStaminaPerSec;

    //Highest stamina by level 15, assume the user went for only stamina, not increasing HP
    public int highestStaminaByLv15;

    public string language;

    // Use this for initialization
    void Start() {

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
        
        //setup the mice
        cursor1 = (Texture2D)Resources.Load("mouse1");
        cursor2 = (Texture2D)Resources.Load("mouse2");
        Cursor.SetCursor(cursor1, new Vector2(2, 2), CursorMode.Auto);

        //Set the background by tier
        switchBackground(1);

        //Debug.Log("thePlayer Created");
        //disable all cameras but the one one at 0
        for (int i = 1; i < theCameras.Length; i++)
        {
            theCameras[i].enabled = false;
            theCanvases[i].enabled = false;
        }
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
            case 1: nameOfBackground = "forest";
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
