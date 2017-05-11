using System;
using NUnit.Framework;

namespace Core.Test
{
    [TestFixture]
    public class CubeTest
    {
        [Test]
        public void TestThrow()
        {
            Cube cube = new Cube();

            for (int i = 0; i < 1000; i++ )
            {
                int n = cube.Throw();
                Assert.That(n, Is.GreaterThanOrEqualTo(1));
                Assert.That(n, Is.LessThanOrEqualTo(6));
            }
        }
    }
}
