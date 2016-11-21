using System;

// TODO: add more of everything: adjectives, weapontypes, items
public static class Gen {

    private static System.Random ran = new System.Random();

    private static string[] weakAdjectives = {
        "Beginner's Luck",
        "Crudeness",
        "Clumsiness",
        "Questing",
        "Danger",
        "Offensiveness",
        "Peculiarity",
        "Potencey",
        "Beauty"
    };
    
    private static string[] strongAdjectives = {
        "Doom",  
        "Annihilation",
        "Terror",
        "Maelstrom",
        "Blood",
        "Divinity",
        "Power",
        "Devastation",
        "Excellence",
        "Fate",
        "Greatness",
        "Importance",
        "Sacredness",
        "Superiority",
        "Truth",
        "Justice",
        "Ultimacy"
    };

    private static string[] meleeWeaponTypes = {
        "Sword",
        "Axe",
        "Hammer",
        "Mallet",
        "Halberd",
        "Dagger",
        "Rapier",
        "Mace"
    };

    private static string[] magicWeaponTypes = {
        "Staff",
        "Wand",
        "Tome",
        "Book",
        "Scepter"
    };

    private static string[] rangedWeaponTypes = {
        "Bow",
        "Crossbow",
        "Darts",
        "Sling"
    };

    private static Item[] junkItems = {
        new Item("Bottle of Goo", 2, true, Item.itemTypes.loot, 10, "Slimey substance in a bottle."),
        new Item("Crumpled Paper", 0.1m, true,Item.itemTypes.loot, 5, "There's nothing written on it."),
        new Item("Metal Scraps", 10, true,Item.itemTypes.loot, 45, "Scrap metal. Could be valuable."),
        new Item("Melted Candle", 0.5m, true,Item.itemTypes.loot, 15, "An old candle."),
        new Item("Dusty Old Lentern", 4, true,Item.itemTypes.loot, 20, "I wonder if it works."),
        new Item("Slightly Damp Rag", 0.3m, true,Item.itemTypes.loot, 7, "Why did I pick this up?"),
        new Item("Oily Boot", 1.2m, true,Item.itemTypes.loot, 10, "Only one."),
        new Item("Bag of Marbles", 5, true,Item.itemTypes.loot, 50, "Something to do, I suppose."),
        new Item("Small Potted Plant", 4, true,Item.itemTypes.loot, 25, "How lovely.")
    };

    private static Item[] valuableItems = {
        new Item("Golden Chalice", 4, true,Item.itemTypes.loot, 150, "A heavy gilded cup."),
        new Item("Ruby", 0.1m, true,Item.itemTypes.loot, 75, "A red gem."),
        new Item("Emerald", 0.1m, true,Item.itemTypes.loot, 100, "A green gem."),
        new Item("Diamond", 0.1m, true,Item.itemTypes.loot, 300, "A clear gem."),
        new Item("Huge Platinum Throne", 50, true,Item.itemTypes.loot, 550, "What a chair! Bloody heavy, though."),
        new Item("Jeweled Crown", 2.5m, true,Item.itemTypes.loot, 175, "Perhaps it belonged to a king.")
    };

    public static string weakAdjective() {
        return weakAdjectives[ran.Next(weakAdjectives.Length)];
    }

    public static string strongAdjective() {
        return strongAdjectives[ran.Next(strongAdjectives.Length)];
    }

    
    public static Item drop(int level) {
        int roll = ran.Next(100);

        if (roll < 25) { // weapon
            return weapon(level);
        } else if (roll < 90) { // junk
            return junkItems[ran.Next(valuableItems.Length)];
        } else { // valuable
            return valuableItems[ran.Next(valuableItems.Length)];
        }
    }

    public static Weapon weapon(int level) {
        int type = ran.Next(3);

        if (type == 0) {
            return magicWeapon(level);
        } else if (type == 1) {
            return meleeWeapon(level);
        } else {
            return rangedWeapon(level);
        }
    }

    private static Weapon initWeapon(int level, Weapon.weaponClass classType) {
        string name = "";

        if (classType == Weapon.weaponClass.Magic) {
            name = magicWeaponTypes[ran.Next(magicWeaponTypes.Length)] ;
        } else if (classType == Weapon.weaponClass.Melee) {
            name = meleeWeaponTypes[ran.Next(meleeWeaponTypes.Length)];
        } else {
            name = rangedWeaponTypes[ran.Next(rangedWeaponTypes.Length)];
        }

        name += " of " + ((level > 9) ? strongAdjective() : weakAdjective());

        decimal weight = Math.Round((decimal)ran.NextDouble() * 10) + 1 + (level-1) * 5;
        bool tradable  = true;
        decimal value  = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(3);

        return new Weapon(
            name,
            weight,
            tradable,
            value,
            level,
            classType,
            type,

            /* Tooltip Text */
            name + "\r\n" +
             Enum.GetName(typeof(Weapon.weaponClass), (int)classType) + " Weapon" + "\r\n" +
            "Weight: "   + weight.ToString() + "\r\n" +
            "Tradable: " + ((tradable) ? "Yes" : "No") + "\r\n" +
            "Value: "    + value.ToString() + "\r\n" +
            "Level: "    + level.ToString()
        );
    }

    public static Weapon magicWeapon(int level) {
        return initWeapon(level, Weapon.weaponClass.Magic);
    }

    public static Weapon meleeWeapon(int level) {
        return initWeapon(level, Weapon.weaponClass.Melee);
    }

    public static Weapon rangedWeapon(int level) {
        return initWeapon(level, Weapon.weaponClass.Ranged);
    }
}
