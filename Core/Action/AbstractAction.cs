using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Action
{
    public interface AbstractAction
    {
        string Name { get; }
        bool ResetOnThrowCubes { get; }
        void Execute(Player player, Player[] allPlayers, GameManager gameManager);
    }
}
