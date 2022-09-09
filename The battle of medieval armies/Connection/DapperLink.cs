using Dapper;
using System.Data.SqlClient;
using The_battle_of_medieval_armies.Army;
using Work_with_SQL_Table__HW_4_;

namespace Dapper_BDSQL.Controller
{
    class DapperLink
    {
        static string link;
        public SqlConnection connection { get; private set; }

        bool Start { get; set; } = true;

        private DapperLink()     //Создание подключения к базе SQL
        {
            connection = new SqlConnection(link);
            connection.Open();      //Открытие соединения с базой SQL
            LogFile.Log("Connection to SQL-base", LogLevel.Information);
        }

        private static DapperLink _instance;     //Внутренняя переменная класса, хранящая соединение с базой 
        public static DapperLink GetInstance(DBSettings settings)     //Метод создания подключения к базе SQL
        {
            if (_instance == null)
            {
                link = settings.ToString();
                _instance = new DapperLink();
            }
            return _instance;
        }

        public void BattleLogging(ArmyBase army, int nameId, string action)          //Добавление нового поставщика продуктов
        {
            if (Start)
            {
                LogFile.Log($"Start adding warrior [{army.Army[nameId].Name}] with action [{action}] to SQL-base", LogLevel.Information);
                int rows = connection.Execute($"INSERT INTO [dbo].[BattleLog]([Action],[NameId],[SoldierTypeId],[ArmorId],[WeapontId],[HP])VALUES (\'{action}\', {nameId + 1},  {army.Army[nameId].Id}, {army.Army[nameId].Armor.Id}, {army.Army[nameId].Weapone.Id}, {army.Army[nameId].HP});");
                if (rows > 0)
                {
                    LogFile.Log($"Warrior [{army.Army[nameId].Name}] with action [{action}] was added to SQL-base!", LogLevel.Information);
                }
                else
                    LogFile.Log($"Warrior [{army.Army[nameId].Name}] with action [{action}] wasn`t added to SQL-base!", LogLevel.Warning);
            }
        }

        public void StopLogging() => Start = false;

        public void StrtLogging() => Start = true;

    }
}
