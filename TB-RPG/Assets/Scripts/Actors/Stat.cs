using System.Collections.Generic;
using System.Xml.Serialization;

// Stat is specifically for the 5 stats - strength, intellect, dexterity, cunning, charisma
// we will likely also need handling here for buffs/debuffs when they expire/are dispelled
public class Stat {

    #region Private Vars

    public int _level;
    private int _effectiveLevel;
    private int _gearLevel;
    private int _buffLevel;
    private int _debuffLevel;

    private decimal _modifier;

    #endregion

    #region Public Vars

    // Max value you can level your stats to will be 100
    public int maxLevel { 
        get { 
            return 100;
        } 
    }

    public int minLevel {
        get {
            return 1;
        }
    }

    // value out of maxvalue
    public int level {
        get {
            return _level;
        }
    }

    // total value including buffs from gear (can go over maxvalue), and debuffs
    public int effectiveLevel {
        get {
            return _effectiveLevel;
        }
    }

    // total value just from gear buffs
    public int gearLevel {
        get {
            return _gearLevel;
        }
    }

    public int buffLevel {
        get {
            return _buffLevel;
        }
    }

    // temporary Stat nerfs, (ex: -10 intellect)
    public int debuffLevel {
        get {
            return _debuffLevel;
        }
    }

    public decimal modifier {
        get {
            return _modifier;
        }
    }

    // list of all active buffs
    [XmlIgnore]
    public List<Buff> buffs = new List<Buff>();

    // list of all active debuffs
    [XmlIgnore]
    public List<Debuff> debuffs = new List<Debuff>();

    #endregion

    #region Constructor & Methods

    public Stat()
    {

    }
    public Stat(int level, int gearlevel) {
        setLevel(level);
        _gearLevel = gearlevel;

        calcEffectiveLevel();
    }

    public void addBuff(Buff b)
    {
        buffs.Add(b);
        calcEffectiveLevel();
    }


    public int countBuff(string buffName)
    {
        int count = 0;
        for(int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].name == buffName)
            {
                count++;
            }
        }
        return count;
    }

    public void clearBuffs()
    {
        buffs.Clear();
        calcEffectiveLevel();
    }


    private void calcEffectiveLevel() {
        _buffLevel = 0;
        _debuffLevel = 0;

        for (int x = 0; x < buffs.Count; x++) {
            _buffLevel += (int) buffs[x].value;
        }

        // count up debuffs
        for (int x = 0; x < debuffs.Count; x++) {
            _debuffLevel += (int) debuffs[x].value;
        }

        // add everything up
        int total = level + gearLevel + buffLevel - debuffLevel;

        // don't go below minLevel (can go above maxLevel though)
        _effectiveLevel = (total > minLevel) ? total : minLevel;

        _modifier = (((decimal)_effectiveLevel) / 100m);
    }

    /// <summary>
    /// Attempts to set level to a specified int. Will not go above maxValue or below minLevel.
    /// </summary>
    /// <param name="newLevel">int to set Level to</param>
    /// <returns>True if set to newLevel, false if newLevel isn't within range</returns>
    public bool setLevel(int newLevel) {
        if (newLevel > maxLevel) {
            _level = maxLevel;
            calcEffectiveLevel();
            return false;
        } else if (newLevel < minLevel) {
            _level = minLevel;
            calcEffectiveLevel();

            return false;
        } else {
            _level = newLevel;
            calcEffectiveLevel();
            return true;
        }

    }

    /// <summary>
    /// Attempts to add one to Stat.level
    /// </summary>
    /// <returns>True if successful, false if the Stat is already capped</returns>
    public bool levelUp() {
        var ret = setLevel(level + 1);
        calcEffectiveLevel();

        return ret;
    }

    /// <summary>
    /// Sets gearLevel, which affects virtualLevel
    /// </summary>
    /// <param name="newGearLevel">int to set gearLevel to</param>
    public void setGearLevel(int newGearLevel) {
        _gearLevel = newGearLevel;

        calcEffectiveLevel();
    }

    #endregion
}
