﻿public class MeleeAbility : Ability {

    public MeleeAbility(string name, string toolTip, decimal stamina)
        : base(name, toolTip, stamina) {
    }

    public override decimal cast() {
        return 0.75m;
    }
}