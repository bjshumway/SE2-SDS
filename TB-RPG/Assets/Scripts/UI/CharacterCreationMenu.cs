using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreationMenu : MonoBehaviour {

    public int currHeadNum;
    public Sprite[] heads;

    public int currHeadColorNum;
    public int[,] colors = new int[4, 4] { {255,255,255,255 },
                                           {75,75,10,255 },
                                           {100,10,10,255 },
                                           {10,100,10,255 },
                                            };


    public void Start()
    {
        Debug.Log("Inside start of CharacterCreationMenu");
        //GameObject.FindWithTag("head").GetComponent<Image>().sprite = Heads[0];

        currHeadColorNum = 0;
        currHeadNum = 0;
        GameObject.FindWithTag("head").GetComponent<Image>().color = new Color32((byte)colors[0,0],
                                                                                 (byte)colors[0,1],
                                                                                 (byte)colors[0,2],
                                                                                 (byte)colors[0,3]);

    }

    public void cycleBodyPart2(string bodyPartAndDirection)
    {
        Debug.Log("Inside cycleBodyPart");
        /*
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        Debug.Log(bodyPartAndDir[0]);
        Debug.Log(bodyPartAndDir[1]);
        Debug.Log("Inside cycleBodyPart");

        if(bodyPartAndDir[1] == "right")
        {
            currHeadNum++;
        }
        else
        {
            currHeadNum--;
        }

        if(currHeadNum < 0)
        {
            currHeadNum = heads.Length - 1;
        } else if(currHeadNum > heads.Length -1)
        {
            currHeadNum = 0;
        }

        GameObject.FindWithTag("head").GetComponent<Image>().sprite = heads[currHeadNum];*/

    }

    public void cycleBodyPartColor(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        Debug.Log(bodyPartAndDir[0]);
        Debug.Log(bodyPartAndDir[1]);
        Debug.Log(colors.Length);
        if (bodyPartAndDir[1] == "right")
        {
            currHeadColorNum++;
        }
        else
        {
            currHeadColorNum--;
        }

        if (currHeadColorNum < 0)
        {
            currHeadColorNum = colors.Length/4 - 1;
        }
        else if (currHeadColorNum > colors.Length/4 - 1)
        {
            currHeadColorNum = 0;
        }

        GameObject.FindWithTag("head").GetComponent<Image>().color = new Color32((byte)colors[currHeadColorNum, 0],
                                                                                 (byte)colors[currHeadColorNum, 1],
                                                                                 (byte)colors[currHeadColorNum, 2],
                                                                                 (byte)colors[currHeadColorNum, 3]);
            
    }

}
