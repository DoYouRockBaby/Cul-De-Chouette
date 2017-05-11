using System;
using NUnit.Framework;
using Moq;
using Core.ThrowingTrigger;
using Core.Action;

namespace Core.Test
{
    [TestFixture]
    public class SuiteTriggerTest
    {
        [Test]
        public void TestShouldTrigger()
        {
            Cube[] cubes1 = createCubeMocks(1, 2, 3);
            Cube[] cubes2 = createCubeMocks(3, 1, 2);
            Cube[] cubes3 = createCubeMocks(1, 2, 1);
            Cube[] cubes4 = createCubeMocks(5, 6, 4);
            Cube[] cubes5 = createCubeMocks(4, 2, 2);

            SuiteTrigger trigger = new SuiteTrigger();

            Assert.That(trigger.ShouldTrigger(cubes1), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes2), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes3), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes4), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes5), Is.EqualTo(false));
        }
        [Test]
        public void TestValues()
        {
            Player player = new Player();
            SuiteTrigger trigger = new SuiteTrigger();

            Player[] allPlayers = new Player[3];
            allPlayers[0] = new Player();
            allPlayers[1] = player;
            allPlayers[2] = new Player();

            foreach(Player singlePlayer in allPlayers) {
                singlePlayer.Score = 100;
            }

            Cube[] cubes = createCubeMocks(2, 3, 4);
            player.Cubes = cubes;

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Suite ! Le dernier joueur à crier \"Grelotte ça picote !\" perdra 10 points."));
            outputMock.Setup(mock => mock.Send("Le joueur perds 10 points."));
            gameManager.Output = outputMock.Object;

            trigger.Call(player, allPlayers, gameManager);

            //Every players have now the action GrelotteCaPicote
            foreach (Player aPlayer in allPlayers)
            {
                Assert.IsTrue(aPlayer.CurrentActions.Exists(e => e.GetType() == typeof(GrelotteCaPicote)));
            }

            //The last player that call the action lost the points
            allPlayers[0].CallAction(typeof(GrelotteCaPicote), allPlayers, gameManager);
            Assert.That(allPlayers[0].Score, Is.EqualTo(100));

            allPlayers[1].CallAction(typeof(GrelotteCaPicote), allPlayers, gameManager);
            Assert.That(allPlayers[1].Score, Is.EqualTo(100));

            allPlayers[2].CallAction(typeof(GrelotteCaPicote), allPlayers, gameManager);
            Assert.That(allPlayers[2].Score, Is.EqualTo(90));

            outputMock.VerifyAll();
        }
        [Test]
        public void TestShouldTriggerWrongCubeNumberException()
        {
            SuiteTrigger trigger = new SuiteTrigger();
            Cube[] cubes = createCubeMocks(5, 2, 2, 5);

            //For ShouldTrigger method
            Assert.Throws<Player.WrongCubeNumberException>(delegate
            {
                trigger.ShouldTrigger(cubes);
            });
        }
        [Test]
        public void TestCallWrongCubeNumberException()
        {
            Player player = new Player();
            SuiteTrigger trigger = new SuiteTrigger();

            Cube[] cubes = createCubeMocks(2, 5);
            player.Cubes = cubes;

            //For Trigger method
            Assert.Throws<Player.WrongCubeNumberException>(delegate
            {
                trigger.Call(player, null, null);
            });
        }
        public Cube[] createCubeMocks(params int[] values)
        {
            Cube[] cubes = new Cube[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                Mock<Cube> mock = new Mock<Cube>();
                mock.SetupGet(e => e.Value).Returns(values[i]);
                cubes[i] = mock.Object;
            }

            return cubes;
        }
    }
}
