namespace ActorNS {
    public class RangedWeapon : Weapon {


        public RangedWeapon(string name, decimal weight, bool tradable, decimal value, int level, All.weaponType type, string toolTip = "")
            : base(name, weight, tradable, value, level, type, toolTip) {

        }
    }
}
