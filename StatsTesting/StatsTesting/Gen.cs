using System;

public static class Gen {

    private Random ran = new Random();

    // TODO: add more of these
    private string[] adjectives = {
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

    public string adjective() {
        return adjectives[ran.Next(adjectives.Length)];
    }

    // TODO:? group all weapons into a single class with a property destinguishing them?
    // it would avoid code duplicates here, and I don't see a downside at the moment
    public MeleeWeapon meleeWeapon(int level) {
        string name = adjective();
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
            name +
            "Weight: " + weight.ToString() +
            "Tradable: " + ((tradable) ? "Yes" : "No") +
            "Value: " + value.ToString() + 
            "Level: " + level.ToString()
        );
    }

    public MagicWeapon magicWeapon(int level) {
        string name = adjective();
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
            name +
            "Weight: " + weight.ToString() +
            "Tradable: " + ((tradable) ? "Yes" : "No") +
            "Value: " + value.ToString() +
            "Level: " + level.ToString()
        );
    }

    public RangedWeapon rangedWeapon(int level) {
        string name = adjective();
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
            name +
            "Weight: " + weight.ToString() +
            "Tradable: " + ((tradable) ? "Yes" : "No") +
            "Value: " + value.ToString() +
            "Level: " + level.ToString()
        );
    }
}
