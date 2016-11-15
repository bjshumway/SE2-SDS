using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopInventoryScript : MonoBehaviour {

    public GameObject ItemSwitchInventory;
    public GameObject WeaponSwitchInventory;
    public GameObject LootSwitchInventory;
    
    public GameObject ItemScrollView;
    public GameObject WeaponScrollView;
    public GameObject LootScrollView;

    public GameObject ItemSwitchShop;
    public GameObject WeaponSwitchShop;

    public GameObject ItemBuyScrollView;
    public GameObject WeaponsBuyScrollView;

    private static ShopInventoryScript s_Instance = null;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Shows the inventory specified by clicking on a switchView button
    public void switchView(GameObject g)
    {
        if (g.name.EndsWith("BuyScrollView"))
        {
            WeaponsBuyScrollView.SetActive(false);
            ItemBuyScrollView.SetActive(false);

            WeaponSwitchShop.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
            ItemSwitchShop.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        }
        else
        {
            WeaponScrollView.SetActive(false);
            ItemScrollView.SetActive(false);
            LootScrollView.SetActive(false);

            ItemSwitchInventory.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
            WeaponSwitchInventory.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
            LootSwitchInventory.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        }

        g.SetActive(true);

        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponent<Image>().color = new Color32(0x92, 0xFB, 0x60, 0xFF); ;
    }


    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static ShopInventoryScript instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first ShopInventoryScript object in the scene.
                s_Instance = FindObjectOfType(typeof(ShopInventoryScript)) as ShopInventoryScript;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("ShopInventoryScript");
                s_Instance = obj.AddComponent(typeof(ShopInventoryScript)) as ShopInventoryScript;
                Debug.Log("Could not locate an ShopInventoryScript object. ShopInventoryScript was Generated Automaticly.");
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
