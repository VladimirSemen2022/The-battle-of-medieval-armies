using System;
using ConsoleApp2.Weapone;
using Dapper_BDSQL.Controller;
using The_battle_of_medieval_armies.Army;
using The_battle_of_medieval_armies.Models.Armor;

namespace ConsoleApp2.Soldiers
{
    abstract public class SoldierBase
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int DamageLevel { get; set; }
        public int Distance { get; set; }
        public WeaponeBase Weapone { get; private set; }
        public ArmorBase Armor { get; set; }
        public bool ALive { get; set; } = true;
        public string Sighn { get; set; }
        public int Id { get; private set; }
        private SoldierBase()
        {}

        protected SoldierBase(string name, int hp, int damage, int distance, WeaponeBase weapone, ArmorBase armor, bool aLive, string sighn, int id)
        {
            DamageLevel = damage;
            HP = hp;
            Weapone = weapone;
            Name = name;
            Distance = distance;
            Armor = armor;
            ALive = aLive;
            Sighn = sighn;
            Id = id;
        }

        public virtual void Move()
        {
            int x = Console.WindowWidth;
            string tmp = $"{Name} move";
            Console.SetCursorPosition(x / 2 - tmp.Length / 2, 25);
            Console.WriteLine(tmp);
        }
        public abstract void GetDamage(SoldierBase solder);
        public virtual void Dead()
        {
            int x = Console.WindowWidth;
            string tmp = $"{Name} dead";
            Console.SetCursorPosition(x/2 - tmp.Length / 2, 25);
            Console.WriteLine(tmp);
        }

        public override string ToString()
        {
            return Sighn;
        }
    }
}
