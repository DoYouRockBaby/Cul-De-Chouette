using System;
using NUnit.Framework;
using Moq;
using Core.Action;

namespace Core.Test.Action
{
    [TestFixture]
    public class ThrowCubesActionTest
    {
        class RemovedTestAction : AbstractAction
        {
            public string Name
            {
                get
                {
                    return "Test action";
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
                return;
            }
        }
        class KeptTestAction : AbstractAction
        {
            public string Name
            {
                get
                {
                    return "Test action";
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
                return;
            }
        }

        [Test]
        public void TestResetAction()
        {
            ThrowCubesAction action = new ThrowCubesAction();

            Player player1 = new Player();
            player1.CurrentActions.Add(action);
            player1.CurrentActions.Add(new RemovedTestAction());
            player1.CurrentActions.Add(new KeptTestAction());

            Player player2 = new Player();
            player2.CurrentActions.Add(new RemovedTestAction());
            player2.CurrentActions.Add(new KeptTestAction());

            Player[] allPlayers = {player1, player2};

            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Les dés ont étés lancés."));
            gameManager.Output = outputMock.Object;

            action.Execute(player1, allPlayers, gameManager);

            assertPlayerHasAction(player1, typeof(KeptTestAction));
            assertPlayerHasNotAction(player1, typeof(RemovedTestAction));
            assertPlayerHasNotAction(player1, typeof(ThrowCubesAction));

            assertPlayerHasAction(player2, typeof(KeptTestAction));
            assertPlayerHasNotAction(player2, typeof(RemovedTestAction));
            assertPlayerHasAction(player2, typeof(ThrowCubesAction));

            outputMock.VerifyAll();
        }

        public void assertPlayerHasAction(Player player, Type actionType)
        {
            Assert.That(player.CurrentActions.FindAll(e => e.GetType() == actionType).Count, Is.GreaterThan(0));
        }

        public void assertPlayerHasNotAction(Player player, Type actionType)
        {
            Assert.That(player.CurrentActions.FindAll(e => e.GetType() == actionType).Count, Is.EqualTo(0));
        }
    }
}
