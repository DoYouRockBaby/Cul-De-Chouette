using Core.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    /// <summary>
    /// Trois dés qui se suivent.
    /// Tous les joueurs doivent taper du poing sur la table en criant “ Grelotte ça picote ! ”, le dernier joueur à le faire perd 10 pts.
    /// </summary>
    public class SuiteTrigger : AbstractTrigger
    {
        public bool ShouldTrigger(Cube[] cubes)
        {
            if (cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            int max = Math.Max(cubes[0].Value, Math.Max(cubes[1].Value, cubes[2].Value));
            int min = Math.Min(cubes[0].Value, Math.Min(cubes[1].Value, cubes[2].Value));
            
            int mid = 0;
            if (cubes[0].Value != min && cubes[0].Value != max)
            {
                mid = cubes[0].Value;
            }
            else if (cubes[1].Value != min && cubes[1].Value != max)
            {
                mid = cubes[1].Value;
            }
            else
            {
                mid = cubes[2].Value;
            }

            return (min + 1) == mid && (mid + 1) == max;
        }
        public void Call(Player player, Player[] allPlayers, GameManager gameManager)
        {
            if (player.Cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            gameManager.Output.Send("Suite ! Le dernier joueur à crier \"Grelotte ça picote !\" perdra 10 points.");

            for(int i = 0; i < allPlayers.Length; i++)
            {
                allPlayers[i].CurrentActions.Add(new GrelotteCaPicote());
            }
        }
    }
}