using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    /// <summary>
    /// La somme de deux dés est égale à la valeur du troisième dé.
    /// La Velute a pour valeur le chiffre le plus élevé des trois.
    /// La valeur de la Velute correspond au double du carré de la Velute :
    /// </summary>
    public class VeluteTrigger : AbstractTrigger
    {
        public bool ShouldTrigger(Cube[] cubes)
        {
            if(cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            int max = Math.Max(Math.Max(cubes[0].Value, cubes[1].Value), cubes[2].Value);

            int sum = 0;
            foreach (Cube cube in cubes)
            {
                if (cube.Value != max)
                {
                    sum += cube.Value;
                }
            }

            return sum == max;
        }
        public void Call(Player player, Player[] allPlayers, GameManager gameManager)
        {
            if (player.Cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            int max = Math.Max(Math.Max(player.Cubes[0].Value, player.Cubes[1].Value), player.Cubes[2].Value);
            player.Score += (int)Math.Pow(max, 2) * 2;

            gameManager.Output.Send("Velute de " + max + " ! Le joueur gagne " + (int)(Math.Pow(max, 2) * 2) + " points.");
        }
    }
}