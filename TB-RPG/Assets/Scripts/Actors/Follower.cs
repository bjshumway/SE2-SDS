
// TODO: add differing levelUp() and stat scaling
public class Follower : UserControllable {
    
    public Follower()
        : base() {

        

        switch (this.id)
        {
            case 2:
                name = "Monet";
                for(int i = 0; i < 5; i++)
                {
                    levelUp();
                }
                break;
            case 3:
                name = "Ashton";
                for (int i = 0; i < 10; i++)
                {
                    levelUp();
                }
                break;
        }
    }
}
