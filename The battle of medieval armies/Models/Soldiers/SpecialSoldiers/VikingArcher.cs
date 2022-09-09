using ConsoleApp2.Soldiers;
using ConsoleApp2.Weapone.TwoHande;
using System;
using The_battle_of_medieval_armies.Models.Armor;

namespace The_battle_of_medieval_armies.Models.Soldiers.SpecialSoldier
{
    class VikingArcher : SoldierBase
    {
        public VikingArcher(string name, int dist) : base(name, 15, 4, dist, new Bow(), new SmallShield(), true, "(@", 2)
        { }
        public override void GetDamage(SoldierBase solder)
        {
            if (this.ALive)
            {
                Random rand = new Random();
                int damage = rand.Next(solder.Weapone.Damage);
                if (this.Armor.Level > 0)
                {
                    if (damage > this.Armor.ProtectionLevel)
                    {
                        this.HP -= (damage - this.Armor.ProtectionLevel);
                        this.Armor.Level -= this.Armor.ProtectionLevel;
                    }
                    else
                        this.Armor.Level -= damage;
                }
                else
                    this.HP -= damage;
                if (this.HP <= 0)
                {
                    this.HP = 0;
                    this.ALive = false;
                    Dead();
                    this.Sighn = "+";
                    this.Name = new string('*', this.Name.Length);
                }
                if (this.Armor.Level < 0)
                    this.Armor.Level = 0;
            }
        }

        public override void Move()
        {
            if (this.Distance > this.Weapone.Range && this.ALive)
            {
                this.Distance -= 2;
            }
        }
        public override string ToString()
        {
            return "viking archer";
        }
    }
}
