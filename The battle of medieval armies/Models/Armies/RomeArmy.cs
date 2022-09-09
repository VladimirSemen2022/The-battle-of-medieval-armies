using Dapper_BDSQL.Controller;
using System;
using System.Linq;

namespace The_battle_of_medieval_armies.Army
{
    class RomeArmy : ArmyBase
    {
        readonly string Name;

        public RomeArmy(int size = 5, int distance = 10) : base(size, 1, distance)
        {
            Name = "The Army of Rome";
        }

        public void Attack(ArmyBase enemy)
        {
            SQLController controller = new SQLController(@"D:\DBSetting.json");
            controller.Download();
            DapperLink newLink = DapperLink.GetInstance(controller.defaultsetting);
            int mySize = this.Army.Count;
            int enemySize = enemy.Army.Count;
            int iterator = mySize;
            if (this.Army.FindAll(x => x.ALive).Any() && enemy.Army.FindAll(x => x.ALive).Any())
            {
                Random rnd = new Random();
                int rand;
                do
                {
                    if (this.Army[iterator - 1].ALive && this.Army[iterator - 1].Weapone.Range >= this.Army[iterator - 1].Distance)
                    {
                        rand = rnd.Next(0, enemySize);
                        if (enemy.Army[rand].ALive)
                        {
                            LogFile.Log($"[{this.Name}] starts attacking", LogLevel.Information);
                            newLink.BattleLogging(this, iterator - 1, "attacks");
                            enemy.Army[rand].GetDamage(this.Army[iterator - 1]);
                            if (!enemy.Army[rand].ALive)
                                newLink.BattleLogging(enemy, rand, "dead");
                            iterator--;
                            LogFile.Log($"[{this.Name}] ends attacking", LogLevel.Information);
                        }
                    }
                    else if (this.Army[iterator - 1].ALive)
                    {
                        Move(iterator-1);
                        iterator--;
                    }
                    else
                        iterator--;
                } while (iterator > 0 && this.Army.FindAll(x => x.ALive).Any() && enemy.Army.FindAll(x => x.ALive).Any()) ;
            }
        }

        public void Move(int iterator)
        {
            LogFile.Log($"[{this.Name}] starts moving", LogLevel.Information);
            SQLController controller = new SQLController(@"D:\DBSetting.json");
            controller.Download();
            DapperLink newLink = DapperLink.GetInstance(controller.defaultsetting);
            newLink.BattleLogging(this, iterator, "moves");
            this.Army[iterator].Move();
            LogFile.Log($"[{this.Name}] ends moving", LogLevel.Information);
        }

        public void End()
        {
            Console.WriteLine("Army of Rome was defeated!");
        }

        public override string ToString()
        {
            return "The Army of Rome";
        }
    }
}
