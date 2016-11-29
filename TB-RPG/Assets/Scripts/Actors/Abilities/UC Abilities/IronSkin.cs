public class IronSkin : Ability {

    public void showAttackAnimation(Monster m) {
        //Program animation here
        //We might have a static class of generic animations that this can refer to
        //Also each monster will contain a reference to its image, to make things easier
    }

    public IronSkin(Actor Owner) : base("Iron Skin", "All party members take 20% less damage from monsters", 100, false, Owner) {

    }

    public override void cast(Actor act = null) {
        bool hasWorked = false;

        foreach (var partyMember in GameMaster.instance.thePlayer.theParty) {
            if (partyMember != null && !partyMember.hasPassive(name)) {
                partyMember.passiveAbilities.Add(this);
                hasWorked = true;
            }
        }

        if (hasWorked) {
            owner.stamina.subtract(stamina);
        } else {
            BattleHints.text = "Iron Skin has already been applied to the party.";
        }
    }
}
