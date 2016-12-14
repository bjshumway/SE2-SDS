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

    public GameObject goldText;
    public GameObject weightText;

    public Inventory theShop;

    public bool isInShopInventory;

    private static ShopInventoryScript s_Instance = null;


    // Use this for initialization
    void Start () {
        theShop = new Inventory(null, "shop", 999999);

        goldText = GameObject.Find("Gold");
        weightText = GameObject.Find("Weight");

        //TODO: add stuff like healing potions, etc.


    }

    // Update is called once per frame
    void Update () {
	
	}


    //Shows the inventory specified by clicking on a switchView button
    public void switchView(GameObject g)
    {
        if (g.name.EndsWith("Buy ScrollView"))
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
        button.GetComponent<Image>().color = new Color32(0x92, 0xFB, 0x60, 0xFF);
    }


    
    public void sellItem(Item item)
    {
        GameObject scrollView = null;
        switch(item.itemType)
        {
            case Item.itemTypes.weapon:
                //Make sure the weapon is not equipped
                if (((Weapon)item).isEquipped)
                    return;
                else
                {
                    scrollView = ShopInventoryScript.instance.WeaponsBuyScrollView;
                    GameObject equip = item.invObject.transform.FindChild("EquipButton").gameObject;
                    equip.SetActive(false);
                    theShop.items.Add(item);
                }
                break;
            case Item.itemTypes.loot:
                scrollView = null;
                break;
            case Item.itemTypes.abilityItem:
                scrollView = ShopInventoryScript.instance.ItemBuyScrollView;
                theShop.items.Add(item);
                break;
        }

        AudioControl.playSound("purchase");
        item.value = (decimal) Mathf.Round((float)item.value / (float) 1.25);

        GameMaster.instance.thePlayer.inventory.sellItem(item);
        updateGoldWeightDisplay();


        if (scrollView == null) //We're selling loot, but there's no scrollview in the shop for loot.
        {
            item.invObject.SetActive(false);
            item.invObject = null;
        }
        else
        {
            Transform tr = scrollView.transform;
            GameObject content = tr.FindChild("Viewport").FindChild("Content").gameObject;
            item.invObject.transform.SetParent(content.transform, false);

            GameObject buySell = item.invObject.transform.FindChild("BuySellButton").gameObject;
            buySell.GetComponentInChildren<Text>().text = "BUY";
            buySell.GetComponent<Button>().onClick.RemoveAllListeners();
            buySell.GetComponent<Button>().onClick.AddListener(delegate { ShopInventoryScript.instance.buyItem(item); });
            item.invObject.transform.FindChild("Cost").GetComponent<Text>().text = item.value.ToString();
        }
    }


    public void buyItem(Item item)
    {
        GameObject scrollView = null;
        if (GameMaster.instance.thePlayer.inventory.gold > item.value) {
            GameMaster.instance.thePlayer.inventory.gold -= item.value;
        } else
        {
            //can't afford it
            return;
        }
        if((GameMaster.instance.thePlayer.inventory.weight + item.weight) > GameMaster.instance.thePlayer.inventory.weightCap)
        {
            //too heavey
            return;
        }

        //still here?
        AudioControl.playSound("coins1");
        item.value = (decimal)Mathf.Round((float)item.value * (float)1.25);


        switch (item.itemType)
        {
            case Item.itemTypes.weapon:
                //Make sure the weapon is not equipped
                if (((Weapon)item).isEquipped)
                    return;
                else
                {
                    scrollView = ShopInventoryScript.instance.WeaponScrollView;
                    GameObject equip = item.invObject.transform.FindChild("EquipButton").gameObject;
                    equip.SetActive(true);
                    GameMaster.instance.thePlayer.inventory.items.Add(item);
                    GameMaster.instance.thePlayer.inventory.weight += item.weight;
                }
                break;
            case Item.itemTypes.abilityItem:
                scrollView = ShopInventoryScript.instance.ItemScrollView;
                GameMaster.instance.thePlayer.inventory.items.Add(item);
                GameMaster.instance.thePlayer.inventory.weight += item.weight;
                break;
        }

        theShop.items.Remove(item);

        updateGoldWeightDisplay();

        Transform tr = scrollView.transform;
        GameObject content = tr.FindChild("Viewport").FindChild("Content").gameObject;
        item.invObject.transform.SetParent(content.transform, false);

        GameObject buySell = item.invObject.transform.FindChild("BuySellButton").gameObject;
        buySell.GetComponentInChildren<Text>().text = "SELL";
        buySell.GetComponent<Button>().onClick.RemoveAllListeners();
        buySell.GetComponent<Button>().onClick.AddListener(delegate { ShopInventoryScript.instance.sellItem(item); });

        item.invObject.transform.FindChild("Cost").GetComponent<Text>().text = item.value.ToString();


    }

    public void equipWeapon(Weapon wpn)
    {
        //Find the UC that corresponds to this item's class, and equip it
        UserControllable[] party = GameMaster.instance.thePlayer.theParty;
        for (int i =0; i < party.Length; i++)
        {
            if(party[i] != null)
            {
                switch(party[i].classType)
                {
                    case UserControllable.classTypes.fighter:
                        if(wpn.classType == Weapon.WeaponClass.Melee)
                        {
                            party[i].equipWeapon(wpn);
                            return;
                        }
                        break;
                    case UserControllable.classTypes.mage:
                        if (wpn.classType == Weapon.WeaponClass.Magic)
                        {
                            party[i].equipWeapon(wpn);
                            return;
                        }
                        break;
                    case UserControllable.classTypes.rogue:
                        if (wpn.classType == Weapon.WeaponClass.Ranged)
                        {
                            party[i].equipWeapon(wpn);
                            return;
                        }
                        break;
                }
            }
        }
    }

    public void goBack()
    {
        isInShopInventory = false;
        AudioControl.playSound("door_open");
        BGM.instance.setMusic(BGM.SongNames.victory);
        OverworldScript.instance.load();
    }

    //Loads the shop/inventory and switches to it
    public void load()
    {
        isInShopInventory = true;
        BGM.instance.setMusic(BGM.SongNames.shop);
        GameMaster.instance.switchCamera(6);

        updateGoldWeightDisplay();

        //Load the images
        UserControllable[] ucArr = GameMaster.instance.thePlayer.theParty;
        for (int i = 0; i < ucArr.Length; i++)
        {
            if (ucArr[i] != null)
            {
                GameObject go = GameObject.Find("ShopInventory UC " + ucArr[i].id);
                go.transform.FindChild("HeadType").GetComponent<Image>().sprite = ucArr[i].headType;
                go.transform.FindChild("HeadType").GetComponent<Image>().color = ucArr[i].headColor;
                GameObject go2 = go.transform.Find("Information").gameObject;
                GameObject go3 = go2.transform.Find("Level").gameObject;
                go3.GetComponent<Text>().text = "LEVEL " + ucArr[i].level.ToString();
                switch (ucArr[i].classType) {
                    case UserControllable.classTypes.rogue:
                        go3.GetComponent<Text>().text += ", ROGUE";
                        break;
                    case UserControllable.classTypes.fighter:
                        go3.GetComponent<Text>().text += ", FIGHTER";
                        break;
                    case UserControllable.classTypes.mage:
                        go3.GetComponent<Text>().text += ", MAGE";
                        break; 
                }
                go2.transform.Find("Name").GetComponent<Text>().text = ucArr[i].name;
            }
        }
    }


    //helper function to show changes in gold / weight
    private void updateGoldWeightDisplay()
    {
        Inventory inv = GameMaster.instance.thePlayer.inventory;
        goldText.GetComponent<Text>().text = "Gold: " + inv.gold.ToString();
        weightText.GetComponent<Text>().text = "Weight: " + inv.weight + " / " + inv.weightCap + "lbs";
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
