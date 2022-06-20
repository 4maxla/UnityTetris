using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class DirectionTests
    {
        [Test]
        public void MoveLeft()
        {
            Assert.AreEqual(expected: new Vector3(-1, 0, 0), actual: Shape.moveLeft);
        }

        [Test]
        public void MoveRight()
        {
            Assert.AreEqual(expected: new Vector3(1, 0, 0), actual: Shape.moveRight);
        }

        [Test]
        public void MoveDown()
        {
            Assert.AreEqual(expected: new Vector3(0, -1, 0), actual: Shape.moveDown);
        }

        [Test]
        public void MoveUp()
        {
            Assert.AreEqual(expected: new Vector3(0, 1, 0), actual: Shape.moveUp);
        }
    }
}
