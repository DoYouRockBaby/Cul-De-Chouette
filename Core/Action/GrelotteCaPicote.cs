using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Action
{
    /// <summary>
    /// Used by Core.ThrowingTrigger.SuiteTrigger
    /// </summary>
    public class GrelotteCaPicote : AbstractAction
    {
        public string Name {
            get {
                return "Grelotte ca picote !";
            }
        }
        public bool ResetOnThrowCubes
        {
            get
            {
                return false;
            }
        }

        public void Execute(Player player, Player[] allPlayers, GameManager gameManager)
        {
            player.RemoveAction(typeof(GrelotteCaPicote));

            //Dont modify the score if there are other players that still have the action
            for(int i = 0 ; i < allPlayers.Length; i++)
            {
                if (allPlayers[i].CurrentActions.FindAll(e => e.GetType() == typeof(GrelotteCaPicote)).Count > 0)
                {
                    return;
                }
            }

            gameManager.Output.Send("Le joueur perds 10 points.");
            player.Score -= 10;
        }
    }
}
