using System;

public static class Gen {

    private static Random ran = new Random();

    // TODO: add more of these?
    private static string[] weakAdjectives = {
        "Beginner's Luck",
        "Crudeness",
        "Clumsiness",
        "Questing",
        "Danger",
        "Offensiveness",
        "Peculiarity",
        "Potencey",
        "Beauty",
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
        new Item("Bottle of Goo", 2, true, 10, "Slimey substance in a bottle."),
        new Item("Crumpled Paper", 0.1m, true, 5, "There's nothing written on it."),
        new Item("Metal Scraps", 10, true, 45, "Scrap metal. Could be valuable."),
        new Item("Melted Candle", 0.5m, true, 15, "An old candle."),
        new Item("Dusty Old Lentern", 4, true, 20, "I wonder if it works."),
        new Item("Slightly Damp Rag", 0.3m, true, 7, "Why did I pick this up?"),
        new Item("Oily Boot", 1.2m, true, 10, "Only one.")
    };

    private static Item[] valuableItems = {
        new Item("Golden Chalice", 4, true, 150, "A heavy gilded cup."),
        new Item("Ruby", 0.1m, true, 75, "A red gem."),
        new Item("Emerald", 0.1m, true, 100, "A green gem."),
        new Item("Diamond", 0.1m, true, 300, "A clear gem."),
        new Item("Huge Platinum Throne", 50, true, 550, "What a chair! Bloody heavy, though.")
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
            return meleeWeapon(level);
        } else if (type == 1) {
            return magicWeapon(level);
        } else {
            return rangedWeapon(level);
        }
    }

    // TODO:? group all weapons into a single class with a property destinguishing them?
    // it would avoid code duplicates here, and I don't see a downside at the moment
    public static MeleeWeapon meleeWeapon(int level) {
        string name = meleeWeaponTypes[ran.Next(meleeWeaponTypes.Length)] + " of " + ((level > 9) ? strongAdjective() : weakAdjective());
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(3);

        return new MeleeWeapon(
            name, 
            weight, 
            tradable,
            value,
            level,
            type,

            /* Tooltip Text */
            name + "\r\n" +
            "Melee Weapon" + "\r\n" +
            "Weight: " + weight.ToString() + "\r\n" +
            "Tradable: " + ((tradable) ? "Yes" : "No") + "\r\n" +
            "Value: " + value.ToString() + "\r\n" +
            "Level: " + level.ToString()
        );
    }

    public static MagicWeapon magicWeapon(int level) {
        string name = magicWeaponTypes[ran.Next(magicWeaponTypes.Length)] + " of " + ((level > 9) ? strongAdjective() : weakAdjective());
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(3);

        return new MagicWeapon(
            name,
            weight,
            tradable,
            value,
            level,
            type,

            /* Tooltip Text */
            name + "\r\n" +
            "Magic Weapon" + "\r\n" +
            "Weight: " + weight.ToString() + "\r\n" +
            "Tradable: " + ((tradable) ? "Yes" : "No") + "\r\n" +
            "Value: " + value.ToString() + "\r\n" +
            "Level: " + level.ToString()
        );
    }

    public static RangedWeapon rangedWeapon(int level) {
        string name = rangedWeaponTypes[ran.Next(rangedWeaponTypes.Length)] + " of " + ((level > 9) ? strongAdjective() : weakAdjective());
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(3);

        return new RangedWeapon(
            name,
            weight,
            tradable,
            value,
            level,
            type,

            /* Tooltip Text */
            name + "\r\n" +
            "Ranged Weapon" + "\r\n" +
            "Weight: " + weight.ToString() + "\r\n" +
            "Tradable: " + ((tradable) ? "Yes" : "No") + "\r\n" +
            "Value: " + value.ToString() + "\r\n" +
            "Level: " + level.ToString()
        );
    }
}
