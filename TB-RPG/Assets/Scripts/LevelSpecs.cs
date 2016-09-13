using UnityEngine;
using System.Collections;

public class LevelSpecs  {
    //The themes for each level
    //Note the current themes are simply placeholders
    public static string[] LEVEL_THEMES =   new string[] {
        "BugWorld",
        "HauntedWorld",
        "DragonWorld"
    }

    public static int NUM_ROOMS_AVG = 12;
    public static int NUM_ROOMS_VARIANCE = 4;
    public static int NUM_ROOMS_AVG_GROWTH_PER_LEVEL = 4;
    public static int NUM_ROOMS_VARIANCE_GROWTH_PER_LEVEL = 2;


    //Gets the number of rooms in the map, based on level
    public static int getNumberOfRooms(int level) {
        return Mathf.RoundToInt(NUM_ROOMS_AVG + NUM_ROOMS_AVG_GROWTH_PER_LEVEL * (level - 1) +
                      (Mathf.RoundToInt(Mathf.value()) * 2 - 1) * (NUM_ROOMS_VARIANCE + NUM_ROOMS_VARIANCE_GROWTH_PER_LEVEL * (level - 1) * Random.value());
    }


    //Returns the monsters types attributed to this level
    //Assumption: Monster contains a static function called monsterTypesByLevel
    public static string[] getMonsterTypes(int level) {
        return Monster.monsterTypesByLevel(level);
    }

    //Returns the room types attributed to this level
    //Assumption: room contains a static function called specialRooms by level
    public static string[] getSpecialRooms(int level) {
        return Room.specialRoomsByLevel(level);
    }

    public static string getTheme(int level)
    {
        return LEVEL_THEMES[level];
    }
}
