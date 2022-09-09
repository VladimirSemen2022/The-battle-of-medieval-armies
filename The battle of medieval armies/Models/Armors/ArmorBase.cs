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
    }
}
