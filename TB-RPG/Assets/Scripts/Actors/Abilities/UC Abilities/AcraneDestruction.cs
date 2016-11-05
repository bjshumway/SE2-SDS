using UnityEngine;
using UnityEngine.UI;

public class ArcaneDestruction : SingleTargetAbility {

    public GameObject ADSelectionButton = GameObject.Find("ArcaneDestructionSelection");

    public override void showAnimation(Monster m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public ArcaneDestruction(Actor Owner) : base("Arcane Destruction", "Deals 100% of the selected damage type",
        "intellect", 1.0m, 100, false, Owner, damageType.fire) {
        ADSelectionButton.SetActive(false);
        //value
        //ADSelectionButton
    }
}