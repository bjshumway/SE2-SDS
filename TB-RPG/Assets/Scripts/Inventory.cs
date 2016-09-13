using System.Collections.Generic;

namespace ActorNS {

    // holds Items
    // TODO: add image field
    public class Inventory {

        #region Private Vars

        private Player _player;
        private string _name;
        private decimal _weightCap;
        private decimal _weight = 0;

        #endregion

        #region Public Vars

        public string name { // small pouch, huge backpack, etc..
            get {
                return _name;
            }
        }

        public decimal weightCap { // how much it can hold
            get {
                return _weightCap;
            }
        }

        public decimal weight { // current weight
            get {
                return _weight;
            }
        }

        public Player player {
            get {
                return _player;
            }
        }

        // actual list of items in inventory
        public List<Item> items = new List<Item>();

        #endregion

        #region Constructors & Methods

        public Inventory(Player player, string name, decimal weightCap) {
            _player = player;
            _name   = name;
            _weightCap = weightCap;
        }

        public Inventory(Player player, string name, decimal weightCap, List<Item> items) {
            _player = player;
            _name   = name;
            _weightCap = weightCap;
            this.items = items;

            calcWeight();
        }

        private void calcWeight() {
            _weight = 0;

            for (int x = 0; x < items.Count; x++) {
                _weight += items[x].weight;
            }
        }

        /// <summary>
        /// Attempts to add an item to Inventory.items
        /// </summary>
        /// <param name="item">Item to add to the Inventory</param>
        /// <returns>True if there is room to add the item</returns>
        /// <remarks>USE THIS METHOD OVER Inventory.items.Add(Item)!!!</remarks>
        public bool addItem(Item item) {
            decimal newWeight = weight + item.weight;

            if (newWeight > weightCap) { // too heavy
                return false; // don't add it
            } else { // there's room
                items.Add(item); // add it
                _weight = newWeight; // update weight
                return true;
            }
        }

        /// <summary>
        /// Deletes an item from Inventory.items
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <remarks>USE THIS METHOD OVER Inventory.items.Remove(Item)!!!</remarks>
        public void deleteItem(Item item) {
            _weight -= item.weight;
            items.Remove(item);
        }

        /// <summary>
        /// Sells an Item, updating the player's gold amount
        /// </summary>
        /// <param name="item">Item to sell</param>
        public void sellItem(Item item) {
            player.gold += item.value;
            deleteItem(item);
        }

        #endregion
    }
}
