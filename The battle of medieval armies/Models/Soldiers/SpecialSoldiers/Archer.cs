using System;
using ConsoleApp2.Weapone.TwoHande;
using The_battle_of_medieval_armies.Models.Armor;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class Archer : SoldierBase
    {
        public Archer(string name, int dist) : base(name, 15, 4, dist, new Bow(), new ChainArmor(), true, "|)", 1)
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
                //Console.WriteLine($"Archer {Name} stands at the point and waiting for enemies!");
                this.Distance -= 2;
            }
        }
        public override string ToString()
        {
            return "archer";
        }
    }
}
