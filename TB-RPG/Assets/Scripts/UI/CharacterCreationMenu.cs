using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreationMenu : MonoBehaviour {

    public int currHeadNum;
    public int currHeadColorNum;



    public void Start()
    {
        Debug.Log("Inside start of CharacterCreationMenu");
        //GameObject.FindWithTag("head").GetComponent<Image>().sprite = Heads[0];


        currHeadColorNum = 0;
        currHeadNum = 0;
        GameObject.FindWithTag("head").GetComponent<Image>().color = new Color32((byte)UserControllableLookConfig.instance.colors[0,0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[0,3]);

    }

    //Cycles a body part, e.g. head, eyes, hair
    public void cycleBodyPart2(string bodyPartAndDirection)
    {
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
            currHeadNum = UserControllableLookConfig.instance.heads.Length - 1;
        } else if(currHeadNum > UserControllableLookConfig.instance.heads.Length -1)
        {
            currHeadNum = 0;
        }

        GameObject.FindWithTag("head").GetComponent<Image>().sprite = UserControllableLookConfig.instance.heads[currHeadNum];
    }

    //Cycles the color of a particular body part
    public void cycleBodyPartColor(string bodyPartAndDirection)
    {
        string[] bodyPartAndDir = bodyPartAndDirection.Split();
        Debug.Log(bodyPartAndDir[0]);
        Debug.Log(bodyPartAndDir[1]);
        Debug.Log(UserControllableLookConfig.instance.colors.Length);
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
            currHeadColorNum = UserControllableLookConfig.instance.colors.Length/4 - 1;
        }
        else if (currHeadColorNum > UserControllableLookConfig.instance.colors.Length/4 - 1)
        {
            currHeadColorNum = 0;
        }

        Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);

        GameObject.FindWithTag("head").GetComponent<Image>().color = newColor;
    }

    public void goToNextScene()
    {
        GameMaster.instance.thePlayer.headType = UserControllableLookConfig.instance.heads[0];
        Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);
        GameMaster.instance.thePlayer.headColor = newColor;


        GameMaster.instance.switchCamera(2);
        Combat.instance.refreshUCSprites();
    }

}
