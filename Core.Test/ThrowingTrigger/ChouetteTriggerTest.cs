using System;
using NUnit.Framework;
using Moq;
using Core.ThrowingTrigger;

namespace Core.Test
{
    [TestFixture]
    public class ChouetteTriggerTest
    {
        [Test]
        public void TestShouldTrigger()
        {
            Cube[] cubes1 = createCubeMocks(1, 2, 3);
            Cube[] cubes2 = createCubeMocks(3, 2, 3);
            Cube[] cubes3 = createCubeMocks(5, 2, 3);
            Cube[] cubes4 = createCubeMocks(2, 5, 5);

            ChouetteTrigger trigger = new ChouetteTrigger();

            Assert.That(trigger.ShouldTrigger(cubes1), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes2), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes3), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes4), Is.EqualTo(true));
        }
        [Test]
        public void TestValue2()
        {
            Player player = new Player();
            ChouetteTrigger trigger = new ChouetteTrigger();

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Chouette de 2 ! Le joueur gagne 4 points."));
            gameManager.Output = outputMock.Object;

            //With a 2 as value
            Cube[] cubes = createCubeMocks(2, 2, 3);
            player.Cubes = cubes;

            trigger.Call(player, null, gameManager);
            Assert.That(player.Score, Is.EqualTo(4));

            outputMock.VerifyAll();
        }
        [Test]
        public void TestValue5()
        {
            Player player = new Player();
            ChouetteTrigger trigger = new ChouetteTrigger();

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Chouette de 5 ! Le joueur gagne 25 points."));
            gameManager.Output = outputMock.Object;

            //With a 5 as value
            Cube[] cubes = createCubeMocks(5, 5, 2);
            player.Cubes = cubes;

            trigger.Call(player, null, gameManager);
            Assert.That(player.Score, Is.EqualTo(25));

            outputMock.VerifyAll();
        }
        [Test]
        public void TestShouldTriggerWrongCubeNumberException()
        {
            ChouetteTrigger trigger = new ChouetteTrigger();
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
            ChouetteTrigger trigger = new ChouetteTrigger();

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
