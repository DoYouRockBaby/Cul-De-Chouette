using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Action
{
    /// <summary>
    /// Used by Core.ThrowingTrigger.ChouetteVeluteTrigger
    /// </summary>
    public class PasMouLeCaillouAction : AbstractAction
    {
        public string Name {
            get {
                return "Pas mou le caillou !";
            }
        }
        public bool ResetOnThrowCubes
        {
            get
            {
                return true;
            }
        }
        public int Value { get; private set; }

        public PasMouLeCaillouAction()
        {
            Value = 0;
        }
        public PasMouLeCaillouAction(int value)
        {
            Value = value;
        }
        public void Execute(Player player, Player[] allPlayers, GameManager gameManager)
        {
            player.Score += Value;

            gameManager.Output.Send("Le joueur gagne " + Value + " points.");

            for(int i = 0 ; i < allPlayers.Length; i++)
            {
                allPlayers[i].RemoveAction(typeof(PasMouLeCaillouAction));
            }
        }
    }
}
