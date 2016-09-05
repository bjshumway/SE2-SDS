namespace ActorNS {

    // Actor is the class from which all characters will inherit
    public class Actor {

        #region Private Vars

        private string _name;
        private string _fullName;
        private Title _title;

        private bool _isAlive = true;
        private int _level = 1;

        #endregion

        #region Public Vars

        public string name {
            get {
                return _name;
            }
        }

        // name + title or title + name
        public string fullName {
            get {
                return _fullName;
            }
        }

        // just added this in for fun, see Title.cs (Title as in Dr. or President or .. THE UNKILLABLE)
        public Title title {
            get {
                return _title;
            }
        }

        public bool isAlive {
            get {
                return _isAlive;
            }
        }

        public int level {
            get {
                return _level;
            }
        }

        // went with a skyrim-type system here
        public Resource health;
        public Resource mana;
        public Resource stamina;

        // stats - all subject to change
        public int strength  = 1; // melee damage
        public int intellect = 1; // magic damage
        public int dexterity = 1; // range damage
        public int cunning   = 1; // crit / dodge chance
        public int charisma  = 1; // buy / sell prices

        #endregion

        #region Constructor & Methods

        /// <summary>
        /// Constructor for Actor
        /// </summary>
        /// <param name="name">Name of the Actor</param>
        /// <param name="title">Title for the Actor, if any</param>
        /// <param name="resources">
        /// Array of Resources corresponding to health, mana, stamina
        /// </param>
        /// <param name="stats">
        /// Array of ints corresponding to the Actor's stats in order:
        /// strength, intellect, dexterity, cunning, charisma
        /// </param>
        public Actor(string name, int level, Title title = null, Resource[] resources = null, int[] stats = null) {
            _name = name;
            _level = level;

            if (title != null) { // title
                setTitle(title);
            } else { // no title
                _fullName = name;
            }

            if (resources != null) { // h/m/s specified
                health  = resources[0];
                mana    = resources[1];
                stamina = resources[2];
            } else { // h/m/s not specified - go by this formula
                int resourceModifier = 100 + (level * 7); // no idea if this formula will be good
                health  = new Resource(resourceModifier);
                mana    = new Resource(resourceModifier);
                stamina = new Resource(resourceModifier);
            }

            if (stats != null) { // stats specified
                setStats(stats);
            }
        }

        /// <summary>
        /// Sets Actor.title with the specified Title, and applies the title to Actor.fullName
        /// </summary>
        /// <param name="newTitle">Title to apply</param>
        public void setTitle(Title newTitle) {
            _title = newTitle;

            if (title.beforeName) {
                _fullName = title.text + " " + name;
            } else {
                _fullName = name + " " + title.text;
            }
        }

        /// <summary>
        /// Clears the current title, setting Actor.title to null, and Actor.fullName to Actor.name
        /// </summary>
        public void clearTitle() {
            _title = null;
            _fullName = name;
        }

        /// <summary>
        /// Sets all stats to the specified values
        /// </summary>
        /// <param name="stats">
        /// Array of ints corresponding to the Actor's stats in order:
        /// strength, intellect, dexterity, cunning, charisma 
        /// </param>
        public void setStats(int[] stats) {
            strength  = stats[0];
            intellect = stats[1];
            dexterity = stats[2];
            cunning   = stats[3];
            charisma  = stats[4];
        }

        /// <summary>
        /// Sets all stats to the specified value
        /// </summary>
        /// <param name="stats">int to set ALL stat values to</param>
        public void setStats(int stats) {
            strength  = stats;
            intellect = stats;
            dexterity = stats;
            cunning   = stats;
            charisma  = stats;
        }

        /// <summary>
        /// Brings the actor back to life with specified percentage of resources
        /// </summary>
        /// <param name="percentResources">
        /// Percentage of health/mana/stamina Actor is res'd with (1.0 for 100%)
        /// </param>
        public void resurrect(double percentResources) {
            health.setValue(health.maxValue * percentResources);
            mana.setValue(mana.maxValue * percentResources);
            stamina.setValue(stamina.maxValue * percentResources);

            _isAlive = true;
        }

        /// <summary>
        /// Kills the Actor, setting all resources to 0
        /// </summary>
        public void kill() {
            health.setValue(0);
            mana.setValue(0);
            stamina.setValue(0);

            _isAlive = false;
        }

        #endregion
    }
}
