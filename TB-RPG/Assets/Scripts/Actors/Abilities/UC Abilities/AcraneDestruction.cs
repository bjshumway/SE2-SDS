using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ArcaneDestruction : SingleTargetAbility {
    [XmlIgnore]
    public GameObject ADSelectionButton = GameObject.Find("ArcaneDestructionSelection");

    public override void showAnimation(Actor m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public ArcaneDestruction():base(){}

    public ArcaneDestruction(Actor Owner) : base("Arcane Destruction", "Deals 100% of the selected damage type. White monsters are weak against fire. Blue - Lightning. Red - Water.",
        "intellect", 1.0m, 100, false, Owner, damageType.fire) {
        ADSelectionButton.SetActive(false);
        //value
        //ADSelectionButton
    }


    public override void onLoad()
    {
        ADSelectionButton.SetActive(true);
    }

    public override void onUnload()
    {
        ADSelectionButton.SetActive(false);
    }
}