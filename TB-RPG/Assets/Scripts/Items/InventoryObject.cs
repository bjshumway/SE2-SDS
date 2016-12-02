using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class InventoryObject  {
    [XmlIgnore]
    public GameObject invObject;

    //Objects are either in shop, or player's inventory
    public bool isInShop;
    
    public InventoryObject()
    {

    }

}
