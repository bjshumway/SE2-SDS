
// TODO: add differing levelUp() and stat scaling
public class Follower : UserControllable {
    
    public Follower(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null)
        : base(name, level, title, resources, statArray) {
    }
}
