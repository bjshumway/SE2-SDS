
// TODO: add differing levelUp() and stat scaling
public class Follower : UserControllable {
    
    public Follower()
        : base() {

        

        switch (this.id)
        {
            case 2:
                name = "Monet";
                remainingStatPoints = initialStatPoints;
                remainingResourcePoints = 1;
                break;
            case 3:
                name = "Ashton";
                remainingStatPoints = initialStatPoints + 10;
                remainingResourcePoints = 1;
                break;
        }
    }
}
