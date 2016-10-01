using UnityEngine;
using System.Collections;

// TODO: add talent system; don't know how to flesh out talentPoints yet
public abstract class UserControllable : Actor {
    private int _talentPoints;

    //Assigns the id to each UserControllable
    protected static int id_increment = 1;
    public AbilityBar abilities;


    private Sprite _headType;
    private Color32 _headColor;
    //This keeps track of whether the image is loaded in the battle screen
    private bool _imageIsInBattleScreen;

    public bool imageIsInBattleScreen
    {
        get;set;
    }

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

        abilities = new AbilityBar(this);

        this.id = id_increment;
        id_increment++;

        isUserControllable = true;


    }


    public UserControllable(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null)
        : base(name, level, title, resources, statArray) {

    }
}
