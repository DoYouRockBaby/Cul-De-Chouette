using System;
using NUnit.Framework;
using Core.Action;
using Moq;

namespace Core.Test
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void TestCallAction()
        {
            Player player = new Player();
            player.Score = 0;
            player.CurrentActions.Add(new PasMouLeCaillouAction(20));

            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            gameManager.Output = outputMock.Object;

            player.CallAction(typeof(PasMouLeCaillouAction), new Player[0], gameManager);

            Assert.That(player.Score, Is.EqualTo(20));
        }

        [Test]
        public void TestWrongCallActionCauseBevue()
        {
            Player player = new Player();
            player.Score = 15;

            GameManager gameManager = new GameManager();
            Mock<Output> outputMock = new Mock<Output>();
            outputMock.Setup(mock => mock.Send("Bevue ! Le joueur perds 10 points."));
            gameManager.Output = outputMock.Object;

            player.CallAction(typeof(PasMouLeCaillouAction), new Player[0], gameManager);

            Assert.That(player.Score, Is.EqualTo(5));

            outputMock.VerifyAll();
        }

        [Test]
        public void TestBevueRemovePoints()
        {
            Player player = new Player();
            player.Score = 15;
            player.Bevue();
            Assert.That(player.Score, Is.EqualTo(5));
            player.Bevue();
            Assert.That(player.Score, Is.EqualTo(0));
        }
    }
}
