using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    public interface AbstractTrigger
    {
        bool ShouldTrigger(Cube[] cubes);
        void Call(Player player, Player[] allPlayers, GameManager gameManager);
    }
}
