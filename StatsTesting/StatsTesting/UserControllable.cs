namespace ActorNS {

    // TODO: add talent system; don't know how to flesh out talentPoints yet
    public abstract class UserControllable : Actor {
        private int _talentPoints;

        public int talentPoints {
            get {
                return _talentPoints;
            }
        }


        public UserControllable(string name, int level, Title title = null, Resource[] resources = null, int[] statArray = null)
            : base(name, level, title, resources, statArray) {

        }
    }
}
