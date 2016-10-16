using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class genericBenchmarkMonster : Monster {

    public genericBenchmarkMonster()
        : base("Generic", "demonSkull", 1, 2, 0, null,
                new Resource[] {
                    new Resource(3),
                    new Resource(100)
                },
                new int[] { 1, 1, 1, 1, 1 }
                )
    {
        Debug.Log("this inside constructor of genericBenchmarkMonster" + this.ToString());
        abilities = new Ability[] { new MonsterAttack(this) };

    }



}
