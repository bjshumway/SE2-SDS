using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuScript : MonoBehaviour
{
    


    public void loadGameButton()
    {
        GameMaster.instance.thePlayer = Player.load("test.xml");
        Player player = GameMaster.instance.thePlayer;
        player.theParty = new UserControllable[] {
            player,
            player.followers[0],
            player.followers[1]
        };

        OverworldScript.instance.load();

    }

    public void startGame()
    {
        AudioControl.playSound("door_open");
        CharacterCreationMenu.load(GameMaster.instance.thePlayer, true);
    }


    public void Start()
    {

        
        
    }
}
