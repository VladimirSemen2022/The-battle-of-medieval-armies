using ConsoleApp2.Soldiers;
using ConsoleApp2.Soldiers.SpecialSoldier;
using ConsoleApp2.Soldiers.Swordsman;
using Dapper_BDSQL.Controller;
using System;
using System.Collections.Generic;
using The_battle_of_medieval_armies.Models.Soldiers.SpecialSoldier;

namespace The_battle_of_medieval_armies.Army
{
    public abstract class ArmyBase
    {
        public int ArmySize { get; set; }

        public List<SoldierBase> Army { get; set; }

        private ArmyBase() { }

        protected ArmyBase (int size, int armyNumber, int distance)
        {
            LogFile.Log($"Start create new army №[{armyNumber}] with size [{size}]", LogLevel.Information);
            if (size > 18)
                size = 18;
            else if (size < 2)
                size = 2;
            if (distance > 15)
                distance = 15;
            else if (distance < 0)
                distance = 0;
            Army = new List<SoldierBase>(ArmySize = size);
            Random rnd = new Random();
            do
            {
                if (armyNumber == 1)
                {
                    SoldierBase[] rome = new SoldierBase[] { new Archer("Archer", distance), new LanceKnight("Heavy knight", distance), new RomeLegionnaire("Legionnaire", distance) };
                    Army.Add(rome[rnd.Next(rome.Length)]);
                    size--;
                }
                else
                {
                    SoldierBase[] vikings = new SoldierBase[] { new VikingArcher("Viking archer", distance), new Berserk("Berserk", distance), new Viking("Viking", distance) };
                    Army.Add(vikings[rnd.Next(vikings.Length)]);
                    size--;
                }
            } while (size != 0);
            LogFile.Log($"New army №[{armyNumber}] with size [{size}] was created sucssessfully", LogLevel.Information);
        }
    }
}
