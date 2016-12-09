using UnityEngine;

public class StealGold : SingleTargetAbility {

    public void showAnimation(Actor m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public StealGold() : base() { }

    public StealGold(Actor Owner) : base("Steal Gold", "Steal some gold from a monster based on Dexterity",
        "dexterity", 0.0m, 50, false, Owner, damageType.none) {

    }
    
    public override void dealEffect(Actor act) {
        var goldStolen = (act as Monster).level + (owner.stats[stat].effectiveLevel * 0.25m);
        GameMaster.instance.thePlayer.inventory.gold += goldStolen;
        BattleHints.text = "Stole " + goldStolen + " Gold";
        Debug.Log("StealGold: Player Current Gold: " + GameMaster.instance.thePlayer.gold);

        owner.stamina.subtract(stamina);
        showAnimation(act);
    }
}