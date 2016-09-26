using System.Windows.Forms;

namespace StatsTesting {
    public partial class Form1 : Form {

        Player player;
        Inventory inventory;

        public Form1() {
            InitializeComponent();

            // init player
            player = new Player("Hero", 1, 100, new Title("Heroic", true));

            // init inventory
            inventory = new Inventory(player, "Backpack", 25);

            // make some items
            Item key = new Item("Key", 0.1m, false, 0, "I wonder what this unlocks.");
            Item junk1 = new Item("Pile of Goo", 5, true, 25, "Yuck.");
            Item junk2 = new Item("Dusty Metal Scraps", 12, true, 37, "Whatever this once was, it's no longer useful.");
            Item junk3 = new Item("Partially Singed Note", 0.1m, true, 1, "\"Danger ahead! Don't\" - is all you can make out.");

            MeleeWeapon sword = new MeleeWeapon("Blunt Sword", 3, true, 30, 25, Weapon.weaponType.highAccuracy, "A dull sword. It's seen better days.");
            
            // add the items to inventory
            inventory.addItem(key);
            inventory.addItem(junk1);
            inventory.addItem(junk2);
            inventory.addItem(junk3);
            inventory.addItem(sword);

            updateUI();
        }

        private void updateUI() {

            // update weight label
            lblWeight.Text = lblWeight.Tag + inventory.weight.ToString() + " / " + inventory.weightCap.ToString();

            lstInventory.Items.Clear();
            for (int x = 0; x < inventory.items.Count; x++) {
                lstInventory.Items.Add(inventory.items[x]); // spit out all the items in inventory.items into lstInventory.Items (ListBox)
            }
        }

        private void btnShowInfo_Click(object sender, System.EventArgs e) {
            if (lstInventory.SelectedIndex != -1) { // if something is selected
                var item = lstInventory.SelectedItem as Item; // cast it to an Item

                // list off the properties
                MessageBox.Show(
                    "Name: "     + item.name + "\n" +
                    "Weight: "   + item.weight.ToString() + "\n" +
                    "Tradable: " + item.tradable.ToString() + "\n" +
                    "Value: "    + item.value.ToString() + "\n" +
                    "Tooltip: "  + item.toolTip
                    );
            }
        }

        private void btnAddItem_Click(object sender, System.EventArgs e) {
            if (inventory.addItem(new Item("A Generic Item", 3, false, 0, "Super generic!"))) { // added
                updateUI();
            } else { // couldn't add
                MessageBox.Show("Can't add the item; there's not enough room in your inventory.");
            }
        }

        private void btnDeleteItem_Click(object sender, System.EventArgs e) {
            if (lstInventory.SelectedIndex != -1) { // if something's selected
                var item = lstInventory.SelectedItem as Item; // cast it to an Item

                inventory.deleteItem(item); // delete
                updateUI();
            }
        }
    }
}
