using System;

public static class Gen {

    private static Random ran = new Random();

    // TODO: add more of these
    private static string[] adjectives = {
        "Doom",
        "Annihilation",
        "Terror",
        "Maelstrom",
        "Blood",
        "Divinity",
        "Power",
        "Knowledge",
        "Wisdom",
        "Strength",
        "Vitality",
        "Dexterity",
        "Cunning",
        "Intellect"
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

    public static string adjective() {
        return adjectives[ran.Next(adjectives.Length)];
    }

    public static Weapon weapon(int level) {
        int type = ran.Next(0, 2);

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
        string name = meleeWeaponTypes[ran.Next(meleeWeaponTypes.Length)] + " of " + adjective();
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(0, 3);

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
        string name = magicWeaponTypes[ran.Next(magicWeaponTypes.Length)] + " of " + adjective();
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(0, 3);

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
        string name = rangedWeaponTypes[ran.Next(rangedWeaponTypes.Length)] + " of " + adjective();
        decimal weight = ((decimal)ran.NextDouble() * 10) + 1;
        bool tradable = true;
        decimal value = (decimal)ran.Next((int)(level * 0.5), (int)(level * 2));
        Weapon.weaponType type = (Weapon.weaponType)ran.Next(0, 3);

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
