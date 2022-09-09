﻿using System;
using ConsoleApp2.Weapone.OneHande;
using The_battle_of_medieval_armies.Models.Armor;

namespace ConsoleApp2.Soldiers.SpecialSoldier
{
    class LanceKnight : SoldierBase
    {
        public LanceKnight(string name, int dist) : base(name, 22, 4, dist, new Lance(), new Kirasa(), true, "*}", 5)
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
                this.Distance -= 1;
            }
        }
        public override string ToString()
        {
            return "lancer knight";
        }
    }
}
