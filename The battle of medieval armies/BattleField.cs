using Dapper_BDSQL.Controller;
using System;
using System.Threading;
using The_battle_of_medieval_armies.Army;

namespace The_battle_of_medieval_armies.Models.Army
{
    class BattleField
    {
        public RomeArmy RArmy { get; set; }
        public VikingArmy VArmy { get; set; }


        public BattleField()
        {
            RArmy = new RomeArmy();
            VArmy = new VikingArmy();
        }

        public BattleField(int size=5, int distance=20)         //Создание противоборствующих армий
        {
            RArmy = new RomeArmy(size, distance);
            VArmy = new VikingArmy(size, distance);
        }

        public void Battle(int firstStep, int deley)            //Основная игра
        {
            //Создание класса Singleton (одиночка) для реализации подключения к базе данных SQL
            LogFile.Log("Read file to SQLBD-way", LogLevel.Information);
            SQLController controller = new SQLController(@"D:\DBSetting.json");
            controller.Download();
            LogFile.Log("Start connection to SQLBD", LogLevel.Information);
            DapperLink newLink = DapperLink.GetInstance(controller.defaultsetting);
            LogFile.Log("Connection to SQLBD is successful", LogLevel.Information);

            newLink.StopLogging();          //Остановка логгирования боя в базе SQL


            //Начало битвы
            ShowBattleField();
            ShowStart();
            if (firstStep == 1)
            {
                newLink.BattleLogging(RArmy, 0, "begins");
                do
                {
                    ShowBattleField();
                    RArmy.Attack(VArmy);
                    Thread.Sleep(deley);
                    ShowBattleField();
                    VArmy.Attack(RArmy);
                    Thread.Sleep(deley);
                } while (RArmy.Army.FindAll(x => x.ALive).Count > 0 && VArmy.Army.FindAll(x => x.ALive).Count > 0);
            }
            else
            {
                newLink.BattleLogging(VArmy, 0, "begins");
                do
                {
                    ShowBattleField();
                    VArmy.Attack(RArmy);
                    Thread.Sleep(deley);
                    ShowBattleField();
                    RArmy.Attack(VArmy);
                    Thread.Sleep(deley);
                } while (RArmy.Army.FindAll(x => x.ALive).Count > 0 && VArmy.Army.FindAll(x => x.ALive).Count > 0);
            }
            ShowBattleField();
            if (RArmy.Army.FindAll(x => x.ALive).Count > 0)
            {
                for (int i = 0; i < RArmy.Army.Count; i++)
                {
                    if (RArmy.Army[i].ALive)
                        newLink.BattleLogging(RArmy, i, "wins");
                }
                ShowEnd("Army of Rome");
            }
            else
            {
                for (int i = 0; i < VArmy.Army.Count; i++)
                {
                    if (VArmy.Army[i].ALive)
                        newLink.BattleLogging(RArmy, i, "wins");
                }
                ShowEnd("Vikings");
            }
        }

        public void ShowBattleField()                           //Показать игровое поле
        {
            Console.Clear();
            int x = Console.WindowWidth;
            int y = Console.WindowHeight;
            string[] game = { "TYPE WARRIORS        HP    ARMOR", "ROME ARMY", "FIELD OF BATTLE", "VIKING ARMY", "X - Exit from game", "Rome warriors", "Viking warriors",
                "", $"Alive warriors - ", $"Dead warriors - " };
            Console.SetCursorPosition(3, 4);
            Console.WriteLine(game[0]);     //"TYPE WARRIORS        HP    ARMOR"
            Console.SetCursorPosition(83, 4);
            Console.WriteLine(game[0]);     //"TYPE WARRIORS        HP    ARMOR"
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(new string('-', x));
            Console.WriteLine();
            Console.SetCursorPosition(x / 6 - game[1].Length / 2, 2);   //"ROME ARMY"
            Console.WriteLine(game[1]);
            Console.SetCursorPosition(x / 2 - game[2].Length / 2, 2);   //"FIELD OF BATTLE"
            Console.WriteLine(game[2]);
            Console.SetCursorPosition(x / 6 * 5 - game[3].Length / 2, 2);   //"VIKING ARMY"
            Console.WriteLine(game[3]);
            Console.SetCursorPosition(x / 12 * 5 - game[5].Length / 2, 4);  //"Rome warriors"
            Console.WriteLine(game[5]);
            Console.SetCursorPosition(x / 12 * 7 - game[6].Length / 2, 4);  //"Viking warriors"
            Console.WriteLine(game[6]);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(new string('-', x));
            Console.SetCursorPosition(0, 28);
            Console.WriteLine(new string('-', x));
            Console.SetCursorPosition(32, 29);
            Console.WriteLine($"{game[8]}{RArmy.Army.FindAll(x => x.ALive).Count + VArmy.Army.FindAll(x => x.ALive).Count}");
            Console.SetCursorPosition(56 + game[8].Length, 29);  //"Warriors out of the battle -"
            Console.WriteLine($"{game[9]}{ RArmy.Army.FindAll(x => !x.ALive).Count + VArmy.Army.FindAll(x => !x.ALive).Count}");
            for (int i = 0; i < 26; i++)
            {
                Console.SetCursorPosition(x / 3, 2 + i);
                Console.WriteLine("|");
                Console.SetCursorPosition(x / 3 * 2, 2 + i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(0, 24);
            Console.WriteLine(new string('-', x));

            ShowRomeArmy();
            ShowVikingArmy();
        }

        public void ShowRomeArmy()                              //Показать римскую армию 
        {
            int step = 1;
            int x = Console.WindowWidth;
            foreach (var item in RArmy.Army)
            {
                Console.SetCursorPosition(2, 5 + step);             //Name
                Console.WriteLine($"{step}. {item.Name}");
                Console.SetCursorPosition(24, 5 + step);            //HP
                Console.WriteLine(item.HP);
                Console.SetCursorPosition(32, 5 + step);            //Armor
                Console.WriteLine(item.Armor.Level);
                Console.SetCursorPosition(x / 3 + 18 - item.Distance, 5 + step);            //Warriors
                Console.WriteLine(item.Sighn);
                step++;
            }
            int archersAlive = RArmy.Army.FindAll(x => x.ALive && x.Name == "Archer").Count;
            int archers = RArmy.Army.FindAll(x => x.Name.Length == 6).Count;
            int legionnairsAlive = RArmy.Army.FindAll(x => x.ALive && x.Name == "Legionnaire").Count;
            int legionnairs = RArmy.Army.FindAll(x => x.Name.Length == 11).Count;
            int knightsAlive = RArmy.Army.FindAll(x => x.ALive && x.Name == "Heavy knight").Count;
            int knights = RArmy.Army.FindAll(x => x.Name.Length == 12).Count;
            Console.SetCursorPosition(2, 25);
            Console.WriteLine($"Archers      alive - {archersAlive}  dead - {archers - archersAlive}");
            Console.SetCursorPosition(2, 26);
            Console.WriteLine($"Legionnairs  alive - {legionnairsAlive}  dead - {legionnairs - legionnairsAlive}");
            Console.SetCursorPosition(2, 27);
            Console.WriteLine($"Knights      alive - {knightsAlive}  dead - {knights - knightsAlive}");
        }

        public void ShowVikingArmy()                            //Показать армию викингов 
        {
            int x = Console.WindowWidth;
            int step = 1;
            foreach (var item in VArmy.Army)
            {
                Console.SetCursorPosition(x / 3 * 2 + 2, 5 + step);
                Console.WriteLine($"{step}. {item.Name}");
                Console.SetCursorPosition(x / 3 * 2 + 24, 5 + step);
                Console.WriteLine(item.HP);
                Console.SetCursorPosition(x / 3 * 2 + 32, 5 + step);
                Console.WriteLine(item.Armor.Level);
                Console.SetCursorPosition(x / 3 * 2 - 18 + item.Distance, 5 + step);
                Console.WriteLine(item.Sighn);
                step++;
            }
            Console.SetCursorPosition(x / 3 * 2 + 2, 25);
            int archersAlive = VArmy.Army.FindAll(x => x.ALive && x.Name == "Viking archer").Count;
            int archers = VArmy.Army.FindAll(x => x.Name.Length == 13).Count;
            int vikingsAlive = VArmy.Army.FindAll(x => x.ALive && x.Name == "Viking").Count;
            int vikings = VArmy.Army.FindAll(x => x.Name.Length == 6).Count;
            int berserksAlive = VArmy.Army.FindAll(x => x.ALive && x.Name == "Berserk").Count;
            int berserks = VArmy.Army.FindAll(x => x.Name.Length == 7).Count;
            Console.WriteLine($"Archers   alive - {archersAlive}  dead - {archers - archersAlive}");
            Console.SetCursorPosition(x / 3 * 2 + 2, 26);
            Console.WriteLine($"Vikings   alive - {vikingsAlive}  dead - {vikings - vikingsAlive}");
            Console.SetCursorPosition(x / 3 * 2 + 2, 27);
            Console.WriteLine($"Berserks  alive - {berserksAlive}  dead - {berserks - berserksAlive}");
        }

        public void ShowEnd(string name)                        //Отображение надписи в конце игры о том кто выиграл
        {
            int x = Console.WindowWidth;
            Console.SetCursorPosition(x / 2 - (name.Length + 5) / 2 - 2, 14);
            Console.WriteLine(new string('-', name.Length + 9));
            Console.SetCursorPosition(x / 2 - (name.Length + 5) / 2 - 2, 15);
            Console.WriteLine($"| {name} WON! |");
            Console.SetCursorPosition(x / 2 - (name.Length + 5) / 2 - 2, 16);
            Console.WriteLine(new string('-', name.Length + 9));
            Console.SetCursorPosition(x / 2 - 10, 26);
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        public void ShowStart()                                 //Отображение надписи в начале игры
        {
            int x = Console.WindowWidth;
            string start = "Press any key to start the game!";
            Console.SetCursorPosition(x / 2 - start.Length / 2, 26);
            Console.WriteLine(start);
            Console.ReadKey();
        }
    }
}
