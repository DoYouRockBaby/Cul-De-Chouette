using Core.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class GameManager
    {
        public virtual Rules Rules { get; set; }
        private Queue<Player> Players = new Queue<Player>();
        public Output Output;

        public void AddPlayer(Player player)
        {
            Players.Enqueue(player);
            if(Players.Count == 1)
            {
                player.CurrentActions.Add(new ThrowCubesAction());
            }
        }
        public void CallAction(Player player, AbstractAction action)
        {
            player.CallAction(action.GetType(), Players.ToArray<Player>(), this);
        }
        public void CallAction(Player player, string actionName)
        {
            foreach(AbstractAction action in Rules.Actions)
            {
                if (action.Name == actionName)
                {
                    CallAction(player, action);
                }
            }
        }
    }
}
