using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// TODO: add talent system; don't know how to flesh out talentPoints yet
public abstract class UserControllable : Actor
{

    private int _talentPoints;

   

    private Sprite _headType;
    private Color32 _headColor;


    public int talentPoints {
        get {
            return _talentPoints;
        }
    }

    public Sprite headType
    {
        get;set;
    }

    public Color32 headColor
    {
        get;set;
    }

    public UserControllable() : base()
    {

    }

    public UserControllable(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null)
        : base(name, level, title, resources, statArray) {

    }



}
