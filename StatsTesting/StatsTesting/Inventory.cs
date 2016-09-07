using System.Collections.Generic;

namespace ActorNS {

    // holds Items
    // TODO: add image field
    public class Inventory {

        #region Private Vars

        private string _name; 
        private double _weightCap;
        private double _weight;

        #endregion

        #region Public Vars

        public string name { // small pouch, huge backpack, etc..
            get {
                return _name;
            }
        }

        public double weightCap { // how much it can hold
            get {
                return _weightCap;
            }
        }

        public double weight { // current weight
            get {
                return _weight;
            }
        }

        // actual list of items in inventory
        public List<Item> items = new List<Item>();

        #endregion

        #region Constructors & Methods

        public Inventory(string name, double weightCap) {
            _name = name;
            _weightCap = weightCap;
        }

        public Inventory(string name, double weightCap, List<Item> items) {
            _name = name;
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
            double newWeight = weight + item.weight;

            if (newWeight > weight) { // too heavy
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

        #endregion
    }
}
