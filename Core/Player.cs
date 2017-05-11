using Core.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Player
    {
        public class WrongCubeNumberException : Exception
        {
            public WrongCubeNumberException() : base() { }
            public WrongCubeNumberException(string message) : base(message) { }
            public WrongCubeNumberException(string message, System.Exception inner) : base(message, inner) { }
        }
        public class BevueException : Exception
        {
            public BevueException() : base() { }
            public BevueException(string message) : base(message) { }
            public BevueException(string message, System.Exception inner) : base(message, inner) { }
        }

        public virtual int Score {
            get {
                return mScore;
            }
            set {
                mScore = Math.Max(0, value);
            }
        }
        public virtual List<AbstractAction> CurrentActions { get; set; }
        public virtual Cube[] Cubes { get; set; }

        private int mScore;
        
        public Player()
        {
            CurrentActions = new List<AbstractAction>();
            Cubes = new Cube[3];
            for(int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i] = new Cube();
            }
        }
        /// <summary> Lorsque le joueur se trompe, il y a bevue et il perd 10 points </summary> 
        public void Bevue()
        {
            Score -= 10;
        }
        public void ThrowCubes()
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].Throw();
            }
        }
        public void CallAction(Type actionType, Player[] allPlayers, GameManager gameManager)
        {
            AbstractAction action = CurrentActions.Find(e => e.GetType() == actionType);

            if(action == null)
            {
                this.Bevue();
                gameManager.Output.Send("Bevue ! Le joueur perds 10 points.");
            }
            else
            {
                action.Execute(this, allPlayers, gameManager);
            }
        }
        public void RemoveAction(Type actionType)
        {
            CurrentActions.RemoveAll(e => e.GetType() == actionType);
        }
    }
}
