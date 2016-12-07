using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilityToolTipHandler : MonoBehaviour {

    public string toolTipText;
    public int cost;
    public GameObject abToolTipArea;


    void Start()
    {
        abToolTipArea = GameObject.Find("AbToolTipArea");
    }

	// Update is called once per frame
	void Update () {

        PointerEventData pe = new PointerEventData(EventSystem.current);
        pe.position = Input.mousePosition;

        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        foreach (RaycastResult h in hits)
        {
            if(h.gameObject == gameObject)
            {
                
                abToolTipArea.GetComponent<Text>().text = toolTipText + "\r\n" +
                                                (cost != -1 ? "Stamina to Cast: " + cost : "");
            }
        }

    }
}
