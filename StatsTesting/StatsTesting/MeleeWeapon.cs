namespace ActorNS {
    public class MeleeWeapon : Weapon {


        public MeleeWeapon(string name, decimal weight, bool tradable, decimal value, int level, All.weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, type, toolTip) {

        }
    }
}
