using UnityEngine;
using System.Collections;

public class Tier {
    public static int tier = 1;
    public static int numBattlesInTier = 0;
    public static int maxBattlesPerTier = 6;
    public static int minBattlesPerTier = 3;
    public static int difficulty = 3;

    public static void goToNextTier()
    {
        tier += 1;
        numBattlesInTier = 0;
        difficulty = 1;
    } 
}
