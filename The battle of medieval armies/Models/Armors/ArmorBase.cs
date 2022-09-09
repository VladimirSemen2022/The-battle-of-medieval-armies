using ConsoleApp2.Weapone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_battle_of_medieval_armies.Models.Armor
{
    public abstract class ArmorBase
    {
        public int Level { get; set; }
        public int ProtectionLevel { get; set; }
        public int Id { get; private set; }

        private ArmorBase()
        { }

        protected ArmorBase(int lvl, int PLvl, int id)
        {
            Level = lvl;                    //Общий уровень защиты брони
            ProtectionLevel = PLvl;         //Уровень снижения урона от оружия
            Id = id;
        }

        public virtual void TakeDamage(WeaponeBase weapone)
        {
            Level = ProtectionLevel - weapone.Damage;
            //Console.WriteLine($"Armor {Name} take damage to {Name}!");
        }
        public virtual void Crash()
        {
            Console.WriteLine($"Kirasa was damaged");
        }
    }
}
