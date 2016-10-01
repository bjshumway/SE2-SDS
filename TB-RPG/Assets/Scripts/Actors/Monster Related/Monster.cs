using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//Commented this out since it wasn't compiling, Ben Shum
//using System.Threading.Tasks;

public class Monster : Actor {

    public GameObject image;
    protected static int id_increment = 1;

    private Item _drop;

    //The difficulty of the monster in the level
    //Ranges from 1 to 4
    //A monster of difficulty 
    public int difficultyInLevel;

    public Item drop {
        get {
            return _drop;
        }
    }

    //Generates a single random number based on the mapTier
    public static Monster getRandomMonsterByLevel(int mapTier)
    {
        //The number of monster types particular to a specific mapTier
        int numMonsterTypes;

        //The index for the monster that will be selected
        int monsterIndex;

        System.Random randNum = new System.Random();

        switch (mapTier)
        {
            case 1:
                numMonsterTypes = 1;
                monsterIndex = randNum.Next(numMonsterTypes);
                switch (monsterIndex) { 
                    case 0:
                    return new genericBenchmarkMonster();
                }
                break;
        }



        System.Console.WriteLine("level out of bounds for function monsterTypesByLevel");
        return null;
    }

    //Randomly creates 1 to 4 Monsters that correspond to the mapLevel
    //Their combined difficultyInLevel should be 4
    public static Monster[] genMonstersByLevel(int mapTier)
    {
        Debug.Log("Inside genMonstersByLevel");
        int sumDifficulty = 0;

        List<Monster> monsters = new List<Monster>();
        while (sumDifficulty != 4) {
            Monster m = getRandomMonsterByLevel(mapTier);
            if ((sumDifficulty + m.difficultyInLevel) <= 4)
            {
                sumDifficulty += m.difficultyInLevel;
                monsters.Add(m);
            }
        }

        return monsters.ToArray();
    }


    public Monster() : base()
    {

    }

    public Monster(string name, int level, int diffInLevel, Title title = null, Resource[] resources = null, int[] stats = null)
        : base(name, level, title, resources, stats) {

        difficultyInLevel = diffInLevel;
        this.id = id_increment;
        id_increment++;
    }

    public override void kill() {
        base.kill();
        _drop = Gen.drop(level);
    }
}