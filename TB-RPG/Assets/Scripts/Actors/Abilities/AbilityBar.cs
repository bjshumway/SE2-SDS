public class AbilityBar {
    private const int ABILITY_COUNT = 3;

    private UserControllable _actor;
    private decimal _lowestStamina;

    public Ability[] abilities = new Ability[ABILITY_COUNT];
    public Ability itemAbility;

    public UserControllable actor {
        get {
            return _actor;
        }
    }

    public decimal lowestStamina {
        get {
            return _lowestStamina;
        }
    }

    public AbilityBar(UserControllable actor) {
        _actor = actor;
    }

    private void calcLowestStam() {
        decimal lowStamina = 0;

        for (int x = 0; x < abilities.Length; x++) {
            if (abilities[x] != null && abilities[x].stamina < lowStamina) {
                _lowestStamina = abilities[x].stamina;
            }
        }
    }

    /// <summary>
    /// Sets an ability at a specified index of the abilities array
    /// </summary>
    /// <param name="ability">Ability to set</param>
    /// <param name="index">Where the ability is going</param>
    public void setAbility(Ability ability, int index) {
        abilities[index] = ability;
        calcLowestStam();
    }
}