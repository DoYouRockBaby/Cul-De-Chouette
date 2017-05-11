using System;
using NUnit.Framework;
using Moq;
using Core.ThrowingTrigger;
using Core.Action;

namespace Core.Test
{
    [TestFixture]
    public class ChouetteVeluteTriggerTest
    {
        [Test]
        public void TestShouldTrigger()
        {
            Cube[] cubes1 = createCubeMocks(1, 2, 3);
            Cube[] cubes2 = createCubeMocks(3, 2, 3);
            Cube[] cubes3 = createCubeMocks(1, 2, 1);
            Cube[] cubes4 = createCubeMocks(3, 3, 6);
            Cube[] cubes5 = createCubeMocks(4, 2, 2);

            ChouetteVeluteTrigger trigger = new ChouetteVeluteTrigger();

            Assert.That(trigger.ShouldTrigger(cubes1), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes2), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes3), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes4), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes5), Is.EqualTo(true));
        }
        [Test]
        public void TestValues()
        {
            Player player = new Player();
            ChouetteVeluteTrigger trigger = new ChouetteVeluteTrigger();

            Player[] allPlayers = new Player[3];
            allPlayers[0] = new Player();
            allPlayers[1] = player;
            allPlayers[2] = new Player();

            Cube[] cubes = createCubeMocks(2, 2, 4);
            player.Cubes = cubes;

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Chouette velutte de 4 ! Le premier joueur à crier \"Pas mou le caillou !\" gagnera 32 points."));
            outputMock.Setup(mock => mock.Send("Le joueur gagne 32 points."));
            gameManager.Output = outputMock.Object;

            trigger.Call(player, allPlayers, gameManager);

            //Every players have now the action PasMouLeCaillouAction
            foreach (Player aPlayer in allPlayers)
            {
                Assert.IsTrue(aPlayer.CurrentActions.Exists(e => e.GetType() == typeof(PasMouLeCaillouAction)));
            }

            //The first player that call the action win the points
            allPlayers[0].CallAction(typeof(PasMouLeCaillouAction), allPlayers, gameManager);
            Assert.That(allPlayers[0].Score, Is.EqualTo(32));

            //The other can call the action for 10 seconds but donc win any points
            allPlayers[1].CallAction(typeof(PasMouLeCaillouAction), allPlayers, gameManager);
            Assert.That(allPlayers[1].Score, Is.EqualTo(0));
            allPlayers[2].CallAction(typeof(PasMouLeCaillouAction), allPlayers, gameManager);
            Assert.That(allPlayers[2].Score, Is.EqualTo(0));
            //PS : we will not test the destruction timer because we want our test to be executed quickly

            outputMock.VerifyAll();
        }
        [Test]
        public void TestShouldTriggerWrongCubeNumberException()
        {
            ChouetteVeluteTrigger trigger = new ChouetteVeluteTrigger();
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
            ChouetteVeluteTrigger trigger = new ChouetteVeluteTrigger();

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
