namespace ActorNS {
    public class MagicWeapon : Weapon {


        public MagicWeapon(string name, decimal weight, bool tradable, decimal value, int level, All.weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, type, toolTip) {

        }
    }
}
