using Game.EnemyAndPlayer;
using NUnit.Framework;

namespace Game.Test
{
    // Будут к Среде
    [TestFixture]
    public class Tests_Object
    {
        Player testPlayer = new Player(10, 10, 0);

        [Test]
        public void CreatePlayer() =>
            Assert.AreEqual(new Player(10, 10, 0).X, testPlayer.X);
    }
}