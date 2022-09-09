namespace The_battle_of_medieval_armies.Models.Armor
{
    class Big_shield : ArmorBase
    {
        public Big_shield(int level = 11, int pLevel = 4) : base(level, pLevel, 5)
        { }

        public override string ToString()
        {
            return "Big shield";
        }
    }
}
