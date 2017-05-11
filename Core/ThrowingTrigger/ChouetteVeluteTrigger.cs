using Core.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ThrowingTrigger
{
    /// <summary>
    /// Une Chouette + une Velute.
    /// La Chouette Velute a la valeur de sa Velute. Le premier joueur qui frappe dans ses mains en criant “Pas mou le caillou!” gagne les points de la Chouette Velute.
    /// Si plusieurs joueurs sont à égalité lors de l'annonce, alors les points de la Chouette Velute sont soustraits des scores des joueurs concernés.
    /// Pour des raisons pratique, on limitera l'appel a pas mou le caillou pendant 10 secondes
    /// </summary>
    public class ChouetteVeluteTrigger : AbstractTrigger
    {
        public bool ShouldTrigger(Cube[] cubes)
        {
            if(cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            //We check if two cubes are equals
            if (cubes[0].Value == cubes[1].Value || cubes[0].Value == cubes[2].Value || cubes[1].Value == cubes[2].Value)
            {
                //We ignore if we have a chouette velute
                int max = Math.Max(cubes[0].Value, Math.Max(cubes[1].Value, cubes[2].Value));
                int min = Math.Min(cubes[0].Value, Math.Min(cubes[1].Value, cubes[2].Value));
                if(min * 2 == max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void Call(Player player, Player[] allPlayers, GameManager gameManager)
        {
            if (player.Cubes.Length != 3)
            {
                throw new Player.WrongCubeNumberException();
            }

            int max = Math.Max(Math.Max(player.Cubes[0].Value, player.Cubes[1].Value), player.Cubes[2].Value);
            for(int i = 0; i < allPlayers.Length; i++)
            {
                allPlayers[i].CurrentActions.Add(new PasMouLeCaillouAction((int)Math.Pow(max, 2) * 2));
            }

            gameManager.Output.Send("Chouette velutte de " + max + " ! Le premier joueur à crier \"Pas mou le caillou !\" gagnera " + (int)Math.Pow(max, 2) * 2 + " points.");

            //Remove every action after 10 seconds
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    allPlayers[i].RemoveAction(typeof(PasMouLeCaillouAction));
                }
                timer.Dispose();
            }, null, 10000, System.Threading.Timeout.Infinite);
        }
    }
}