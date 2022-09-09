using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_battle_of_medieval_armies.Models.Armor
{
    class Kirasa : ArmorBase
    {
        public Kirasa(int level=11, int pLevel=4):base (level, pLevel, 3)
        { }

        public override string ToString()
        {
            return "kirasa";
        }
    }
}
