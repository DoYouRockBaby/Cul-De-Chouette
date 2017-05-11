using System;
using NUnit.Framework;
using Moq;
using Core.ThrowingTrigger;

namespace Core.Test
{
    [TestFixture]
    public class CulDeChouetteTriggerTest
    {
        [Test]
        public void TestShouldTrigger()
        {
            Cube[] cubes1 = createCubeMocks(1, 2, 3);
            Cube[] cubes2 = createCubeMocks(3, 3, 3);
            Cube[] cubes3 = createCubeMocks(5, 2, 3);
            Cube[] cubes4 = createCubeMocks(5, 5, 5);

            CulDeChouetteTrigger trigger = new CulDeChouetteTrigger();

            Assert.That(trigger.ShouldTrigger(cubes1), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes2), Is.EqualTo(true));
            Assert.That(trigger.ShouldTrigger(cubes3), Is.EqualTo(false));
            Assert.That(trigger.ShouldTrigger(cubes4), Is.EqualTo(true));
        }
        [Test]
        public void TestValue2()
        {
            Player player = new Player();
            CulDeChouetteTrigger trigger = new CulDeChouetteTrigger();

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Cul de chouette de 2 ! Le joueur gagne 60 points."));
            gameManager.Output = outputMock.Object;

            //With a 2 as value
            Cube[] cubes = createCubeMocks(2, 2, 2);
            player.Cubes = cubes;

            trigger.Call(player, null, gameManager);
            Assert.That(player.Score, Is.EqualTo(60));

            outputMock.VerifyAll();
        }
        [Test]
        public void TestValue5()
        {
            Player player = new Player();
            CulDeChouetteTrigger trigger = new CulDeChouetteTrigger();

            //Text displayed
            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Cul de chouette de 5 ! Le joueur gagne 90 points."));
            gameManager.Output = outputMock.Object;

            //With a 5 as value
            Cube[] cubes = createCubeMocks(5, 5, 5);
            player.Cubes = cubes;

            trigger.Call(player, null, gameManager);
            Assert.That(player.Score, Is.EqualTo(90));

            outputMock.VerifyAll();
        }
        [Test]
        public void TestShouldTriggerWrongCubeNumberException()
        {
            CulDeChouetteTrigger trigger = new CulDeChouetteTrigger();
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
            CulDeChouetteTrigger trigger = new CulDeChouetteTrigger();

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
