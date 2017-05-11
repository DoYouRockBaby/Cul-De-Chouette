using Core.ThrowingTrigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Action
{
    public class ThrowCubesAction : AbstractAction
    {
        public string Name {
            get {
                return "Lancer les des";
            }
        }
        public bool ResetOnThrowCubes
        {
            get
            {
                return true;
            }
        }

        public void Execute(Player player, Player[] allPlayers, GameManager gameManager)
        {
            player.ThrowCubes();

            gameManager.Output.Send("Les dés ont étés lancés.");
            gameManager.Output.Send(player.Cubes[0].Value + " " + player.Cubes[1].Value + " " + player.Cubes[2].Value);

            foreach(AbstractTrigger trigger in gameManager.Rules.ThrowingTriggers)
            {
                if (trigger.ShouldTrigger(player.Cubes))
                {
                    trigger.Call(player, allPlayers, gameManager);
                }
            }

            //Remove all removable actions
            int i = 0;
            int nextPlayerIndex = 0;
            foreach(Player singlePlayer in allPlayers) {
                singlePlayer.CurrentActions.RemoveAll(e => e.ResetOnThrowCubes);

                if (player == singlePlayer)
                {
                    nextPlayerIndex = ++i % allPlayers.Length;
                }
                else
                {
                    i++;
                }
            }

            //Add throw action to the player just after the current player
            allPlayers[nextPlayerIndex].CurrentActions.Add(new ThrowCubesAction());
        }
    }
}
