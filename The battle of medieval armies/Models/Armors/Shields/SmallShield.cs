namespace The_battle_of_medieval_armies.Models.Armor
{
    class SmallShield : ArmorBase
    {
        public SmallShield(int level = 5, int pLevel = 2) : base(level, pLevel, 6)
        { }

        public override string ToString()
        {
            return "Small shield";
        }
    }
}
