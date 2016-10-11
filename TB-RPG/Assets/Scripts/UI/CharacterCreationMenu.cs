using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreationMenu : MonoBehaviour {

    public int currHeadNum;
    public int currHeadColorNum;



    public void Start()
    {
        //Debug.Log("Inside start of CharacterCreationMenu");
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
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log("Inside cycleBodyPart");

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
        //Debug.Log(bodyPartAndDir[0]);
        //Debug.Log(bodyPartAndDir[1]);
        //Debug.Log(UserControllableLookConfig.instance.colors.Length);
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
        UserControllable uC = GameMaster.instance.thePlayer;
        uC.headType = UserControllableLookConfig.instance.heads[0];
        Color32 newColor = new Color32((byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 0],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 1],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 2],
                                                                                 (byte)UserControllableLookConfig.instance.colors[currHeadColorNum, 3]);
        uC.headColor = newColor;
        uC.battleHead.enabled = true;
        uC.battleHealthBar.enabled = true;
        uC.battleStaminaBar.enabled = true;

        GameObject.Find("Battle UC " + uC.id + " HealthBar").SetActive(true);
        GameObject.Find("Battle UC " + uC.id + " StaminaBar").SetActive(true);


        uC.battleHead.sprite = uC.headType;
        uC.battleHead.color = uC.headColor;


        GameMaster.instance.switchCamera(2);


        UserControllable[] theParty = GameMaster.instance.thePlayer.theParty;
        //Debug.Log(theParty.ToString());
        
    }

}
