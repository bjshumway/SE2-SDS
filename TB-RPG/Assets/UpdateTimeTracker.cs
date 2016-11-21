using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTimeTracker : MonoBehaviour {

    public GameObject timeObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(ShopInventoryScript.instance.isInShopInventory)
        {
            float t = Time.realtimeSinceStartup;
            string hrs = (t / 60 / 60).ToString("00");// >= 1 ? (t / 60 / 60).ToString("00") + ":" : "");
            string min = (t / 60).ToString("00");// >= 1 ? (t / 60 ).ToString("00") + ":" : "0");
            string sec = (t % 60).ToString("00");// != 0 ? (t % 60).ToString("00")");

            timeObj.GetComponent<Text>().text = "Time: " + hrs + ":" + min + ":" + sec;
        }
    }
}
