using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    /// <summary>
    /// Deux dés identiques.
    /// La Chouette a pour valeur le chiffre des deux dés identiques.
    /// La valeur de la Chouette correspond au carré de la Chouette.
    /// </summary> 
    public class ChouetteTrigger : AbstractTrigger
    {
        public bool ShouldTrigger(Cube[] cubes)
        {
            if(cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            //We check if two cubes are equals
            return cubes[0].Value == cubes[1].Value || cubes[0].Value == cubes[2].Value || cubes[1].Value == cubes[2].Value;
        }
        public void Call(Player player, Player[] allPlayers, GameManager gameManager)
        {
            if (player.Cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            if (player.Cubes[0].Value == player.Cubes[1].Value)
            {
                player.Score += (int)Math.Pow(player.Cubes[0].Value, 2);
                gameManager.Output.Send("Chouette de " + player.Cubes[0].Value + " ! Le joueur gagne " + (int)Math.Pow(player.Cubes[0].Value, 2) + " points.");
            }
            else if (player.Cubes[0].Value == player.Cubes[2].Value)
            {
                player.Score += (int)Math.Pow(player.Cubes[0].Value, 2);
                gameManager.Output.Send("Chouette de " + player.Cubes[0].Value + " ! Le joueur gagne " + (int)Math.Pow(player.Cubes[0].Value, 2) + " points.");
            }
            else if (player.Cubes[1].Value == player.Cubes[2].Value)
            {
                player.Score += (int)Math.Pow(player.Cubes[1].Value, 2);
                gameManager.Output.Send("Chouette de " + player.Cubes[1].Value + " ! Le joueur gagne " + (int)Math.Pow(player.Cubes[1].Value, 2) + " points.");
            }
        }
    }
}