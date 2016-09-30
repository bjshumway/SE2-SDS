using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Commented this out since it wasn't compiling, Ben Shum
//using System.Threading.Tasks;

public class Monster : Actor {
    private Item _drop;

    public Item drop {
        get {
            return _drop;
        }
    }

    
    public static string[] monsterTypesByLevel(int level)
    {
        switch(level)
        {
            case 1:
                return new string[] { "spider" };
            case 2:
                return new string[] { "lich" };
            case 3:
                return new string[] { "dragon" };
            case 4:
                return new string[] { "god" };
        }

        System.Console.WriteLine("level out of bounds for function monsterTypesByLevel");
        return null;
    }


    public Monster() : base()
    {

    }

    public Monster(string name, int level, Title title = null)
        : base(name, level, title, null, null) {
            setStatLevels(level + 10); // don't know if this is a good formula
    }

    public override void kill() {
        base.kill();

        _drop = Gen.drop(level);
    }
}