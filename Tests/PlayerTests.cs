using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

using DominantSpecies;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void TestStartingGenePool()
        {
            Assert.AreEqual(35, new Player(Animal.Amphibian, 6).GenePool);
            Assert.AreEqual(40, new Player(Animal.Amphibian, 5).GenePool);
            Assert.AreEqual(45, new Player(Animal.Amphibian, 4).GenePool);
            Assert.AreEqual(50, new Player(Animal.Amphibian, 3).GenePool);
            Assert.AreEqual(55, new Player(Animal.Amphibian, 2).GenePool);
        }
        
        [Test]
        public void TestStartingActionPawns()
        {
            Assert.AreEqual(3, new Player(Animal.Amphibian, 6).ActionPawns);
            Assert.AreEqual(4, new Player(Animal.Amphibian, 5).ActionPawns);
            Assert.AreEqual(5, new Player(Animal.Amphibian, 4).ActionPawns);
            Assert.AreEqual(6, new Player(Animal.Amphibian, 3).ActionPawns);
            Assert.AreEqual(7, new Player(Animal.Amphibian, 2).ActionPawns);
        }
        
        [Test()]
        public void TestSimpleDominanceScore ()
        {
            Map m = new Map();
            Player p = new Player(Animal.Insect);
            
            // Create a known dominance
            var tile = m.tiles[3,3];
            tile.Terrain = Tile.TerrainType.Mountain;
            var chits = m.ChitsFor(tile);
            chits[0].Element = Chit.ElementType.Grass;
            tile.Species[(int) Animal.Insect] = 1;
            
            // Dominance should be 2
            Assert.AreEqual(2, p.DominationScoreOn(m, tile));
        }
    }
}

