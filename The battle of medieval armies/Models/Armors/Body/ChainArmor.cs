using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_battle_of_medieval_armies.Models.Armor
{
    class ChainArmor : ArmorBase
    {
        public ChainArmor(int level = 5, int pLevel = 3) : base(level, pLevel, 2)
        { }

        public override string ToString()
        {
            return "Chain armor";
        }
    }
}
