using Core.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    /// <summary>
    /// Trois dés identiques.
    /// La valeur du Cul de Chouette correspond à 40 pts + 10 * la valeur du Cul de Chouette
    /// </summary>
    public class CulDeChouetteTrigger : AbstractTrigger
    {
        public bool ShouldTrigger(Cube[] cubes)
        {
            if (cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            //We check if two cubes are equals
            return cubes[0].Value == cubes[1].Value && cubes[0].Value == cubes[2].Value;
        }
        public void Call(Player player, Player[] allPlayers, GameManager gameManager)
        {
            if (player.Cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            player.Score += player.Cubes[0].Value * 10 + 40;

            gameManager.Output.Send("Cul de chouette de " + player.Cubes[0].Value + " ! Le joueur gagne " + (player.Cubes[0].Value * 10 + 40) + " points.");
        }
    }
}