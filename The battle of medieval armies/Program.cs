using Dapper_BDSQL.Controller;
using System;
using The_battle_of_medieval_armies.Models.Army;

namespace Army__middle_ages_battle_
{
    class Program
    {
        static void Main(string[] args)
        {
            LogFile.StopLogging();      //Запись лога в файл остановлена, если необходимо то можно включить запись в лог-файл

            LogFile.Log("----------------------------------------------------------------", LogLevel.Information);
            LogFile.Log("Program start", LogLevel.Information);

            int distance = 15;      //Первоначальная дистация между армиями (не более 20)
            int armySize = 18;      //Количество воинов в армии (не более 18)

            try
            {
                //Создание игровой среды
                BattleField newBattle = new BattleField(armySize, distance);

                //Начало битвы между армиями
                Random whoFirst = new Random();
                newBattle.Battle(whoFirst.Next(1, 3), 300);     //Задаются два параметра 1-кто ходит первый и 2-задержка между ходами армий в миллисекундах
            }
            catch (Exception ex)
            {
                LogFile.Log(ex.Message, LogLevel.Error);
                LogFile.Log("Program end", LogLevel.Error);
            }
            Console.Clear();
            Console.WriteLine("THE END");
            LogFile.Log("Program end", LogLevel.Information);
        }
    }
}
