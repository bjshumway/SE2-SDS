using UnityEngine;
using System.Collections;

public class Map {
    public static int tier;
    private static Room[,] rooms;

    private static int MAP_HEIGHT = 11;
    private static int MAP_WIDTH = 11;

    //Helper initBattleRooms recursive function
    private static int numBattleRooms;
    private static int numBattleRoomsPlaced;
    private static ArrayList orderOfRoomPlacings;

    
    public enum levelThemes {
        CritterWorld,
        HauntedWorld,
        EvilWorld
    }

    public static void generateMapByTeir()    {
        rooms = new Room[MAP_WIDTH, MAP_HEIGHT]; //10 by

        int numRooms = LevelSpecs.getNumberOfRooms(tier);
        string[] specRooms = LevelSpecs.getSpecialRooms(tier);

        numBattleRooms = numRooms - specRooms.Length;
        numBattleRoomsPlaced = 0;
        //First place down battle rooms, starting at the center of the map
        int currX = (int)Mathf.Floor(MAP_WIDTH / 2);
        int currY = (int)Mathf.Floor(MAP_HEIGHT / 2);

        initBattleRooms(currX, currY);
        
    }

    private static void initBattleRooms(int x, int y) {
        if(numBattleRooms == numBattleRoomsPlaced) {
            return;
        }

        //1 - right, 2 - left, 3 - up, 4 - down
        int[] possibleDirs = new int[4];

        //check for right
        if ((x + 1) < MAP_WIDTH) {
            if (rooms[x + 1, y] == null)
            {
                possibleDirs[0] = 1;
            } else
            {
                possibleDirs[0] = 0;
            }
        }
        else
        {
            possibleDirs[0] = 0;
        }

        //check for left
        if (x - 1 >= 0) {
            if (rooms[x - 1, y] == null)
            {
                possibleDirs[1] = 1;
            }
            else
            {
                possibleDirs[1] = 0;
            }
        } else
        {
            possibleDirs[1] = 0;
        }

        //check for up
        if (y - 1 >= 0)
        {
            if (rooms[x, y - 1] == null)
            {
                possibleDirs[3] = 1;
            }
            else
            {
                possibleDirs[3] = 0;
            }
        }
        else
        {
            possibleDirs[3] = 0;
        }

        //check for down
        if (y + 1 < MAP_HEIGHT)
        {
            if (rooms[x, y + 1] == null)
            {
                possibleDirs[4] = 1;
            }
            else
            {
                possibleDirs[4] = 0;
            }
        }
        else
        {
            possibleDirs[4] = 0;
        }

        //rule out 4-block cases for possibleDirs here
        possibleDirs = ruleOut4BlockCases(possibleDirs, x, y);

        if (possibleDirs[0] == 0 && possibleDirs[1] == 0 && possibleDirs[2] == 0 && possibleDirs[3] ==0)
        {
            //Go back a space;
            orderOfRoomPlacings.RemoveAt(orderOfRoomPlacings.Count - 1);
            int[] priorRoom = (int[]) orderOfRoomPlacings[orderOfRoomPlacings.Count - 1];
            initBattleRooms(priorRoom[0], priorRoom[1]);
        } else
        {
            bool pickedPlace = false;
            int dir = 0; //Initialized to 0 so that the switch statement below compiles
            while (!pickedPlace)
            {
                dir = (int) Mathf.Floor(Random.value * 4);
                if( possibleDirs[dir] == 1 ){
                    pickedPlace = true;
                }
            }

            switch(dir)
            {
                case 1:
                    x += 1;
                    break;
                case 2:
                    x -= 1;
                    break;
                case 3:
                    y -= 1;
                    break;
                case 4:
                    y += 1;
                    break;
            }

            //Todo: make this new battleroom, not new room.
            rooms[x,y] = new Room();

            orderOfRoomPlacings.Add(new int[x,y]);
            initBattleRooms(x, y);
        }
        
    }

    //Rules out building into a direction that creates a 4-block
    //Assumption: moving in that direction will not be out-of-bounds for the map
    private static int[] ruleOut4BlockCases(int[] possibleDirs,int x,int y)
    {
        //right
        if(possibleDirs[0] == 1)
        {
            //Check top right block
            if(y > 0)
            {
                if(rooms[x+1, y - 1] != null && rooms[x, y-1] != null)
                {
                    possibleDirs[0] = 0;
                }
            }

            //Check for bottom right block
            if (y < MAP_HEIGHT - 1)
            {
                if (rooms[x + 1, y + 1] != null && rooms[x, y + 1] != null)
                {
                    possibleDirs[0] = 0;
                }
            }
        }

        //left
        if (possibleDirs[1] == 1)
        {
            //Check top left block
            if (y > 0)
            {
                if (rooms[x - 1, y - 1] != null && rooms[x, y - 1] != null)
                {
                    possibleDirs[0] = 0;
                }
            }

            //Check for bottom left block
            if (y < MAP_HEIGHT - 1)
            {
                if (rooms[x - 1, y + 1] != null && rooms[x, y + 1] != null)
                {
                    possibleDirs[0] = 0;
                }
            }
        }


        //Up
        if (possibleDirs[2] == 1)
        {
            //Check top left block
            if (x > 0)
            {
                if (rooms[x - 1, y - 1] != null && rooms[x - 1, y] != null)
                {
                    possibleDirs[2] = 0;
                }
            }

            //Check for top right block
            if (x < MAP_WIDTH - 1)
            {
                if (rooms[x + 1, y - 1] != null && rooms[x + 1, y] != null)
                {
                    possibleDirs[2] = 0;
                }
            }
        }

        //down
        if (possibleDirs[3] == 1)
        {
            //Check bottom left block
            if (x > 0)
            {
                if (rooms[x - 1, y + 1] != null && rooms[x - 1, y] != null)
                {
                    possibleDirs[3] = 0;
                }
            }

            //Check for bottom right block
            if (x < MAP_HEIGHT - 1)
            {
                if (rooms[x + 1, y + 1] != null && rooms[x + 1, y] != null)
                {
                    possibleDirs[3] = 0;
                }
            }
        }
        
        return possibleDirs;
    }

}




