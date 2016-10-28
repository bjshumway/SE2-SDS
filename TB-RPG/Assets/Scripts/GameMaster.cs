using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour {

    public Camera[] theCameras;
    public Canvas[] theCanvases;


    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameMaster s_Instance = null;

    public Player thePlayer;
    public Map theMap;

    public Texture2D cursor1;
    public Texture2D cursor2;

	// Use this for initialization
	void Start () {
        thePlayer = new Player();

        //thePlayer.passiveAbilities.Add(new CounterAttack(thePlayer));
        thePlayer.weapon = new MeleeWeapon("rusty sword", 10, false, 1, 1, Weapon.weaponClass.Melee, Weapon.weaponType.balanced, "You found this sword on a long-forgotten battlefield.");



        cursor1 = (Texture2D)Resources.Load("mouse1");
        cursor2 = (Texture2D)Resources.Load("mouse2");
        Cursor.SetCursor(cursor1, new Vector2(0, 0), CursorMode.Auto);

        switchBackground(2);

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
            case 1: nameOfBackground = "forest"; //TODO: get actual name
                break;
            case 2: nameOfBackground = "cave background"; 
                break;
            case 3: nameOfBackground = "graveyard";
                break;
        }

        Debug.Log(tier);
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
