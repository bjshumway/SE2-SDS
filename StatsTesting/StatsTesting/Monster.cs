using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Monster : Actor {
    private Item _drop;

    public Item drop {
        get {
            return _drop;
        }
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