namespace ActorNS {

    // TODO: add talent point system
    public class Player : Actor {
        public decimal gold;

        // I'm thinking now that the constructor for Actor needs to be redone.
        public Player(string name, int level, decimal gold, Title title = null, Resource[] resources = null, int[] statArray = null)
            : base(name, level, title, resources, statArray) {

            this.gold = gold;
        }

    }
}
