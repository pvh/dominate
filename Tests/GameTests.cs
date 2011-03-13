using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

using DominantSpecies;

namespace Tests
{
    [TestFixture()]
    public class GameTests
    {
        internal Game g;
        internal Tile tile;
        internal Chit[] chits;
        
        [SetUp]
        public void SetUp() {
            // no default setup
            g = new Game(false);
            tile = g.map.tiles[3, 3];
            chits = g.map.ChitsFor(tile);
            
            // Be sure we're dealing with a tile that has nothing configured on it
            Assert.IsTrue(chits.All(chit => 
                                    (chit.Element == Chit.ElementType.None
                                     || 
                                     chit.Element == Chit.ElementType.Invalid)));
            Assert.AreEqual(Tile.TerrainType.Empty, tile.Terrain);
        }
    }
    
    [TestFixture()]
    public class GameDominationTests : GameTests
    {
        
        [Test()]
        public void TestDominatedBy ()
        {
            // Add a grub and a species
            chits.First().Element = Chit.ElementType.Grub;
            tile.Species[(int) Animal.Arachnid] = 1;

            // A default spider should now dominate
            var dominator = g.DominatedBy(tile);
            Assert.AreNotEqual(null, dominator);            
            Assert.AreEqual(Animal.Arachnid, dominator.Animal);
        }
        
        [Test()]
        public void TestNobodyPresent ()
        {
            // Add a grub
            chits.First().Element = Chit.ElementType.Grub;

            // A present spider should now dominate, but nobody is there, so no domination
            var dominator = g.DominatedBy(tile);
            Assert.AreEqual(null, dominator);
        }
        
        [Test()]
        public void TestTiedForDomination ()
        {
            // Add a spider, a bird, a grub and a seed
            tile.Species[(int)Animal.Arachnid] = 1;
            chits.First().Element = Chit.ElementType.Grub;
            tile.Species[(int)Animal.Bird] = 1;
            chits.ElementAt(1).Element = Chit.ElementType.Seed;

            // They should be equally dominant
            var arachnid = new Player(Animal.Arachnid);
            Assert.AreEqual(2, arachnid.DominationScoreOn(g.map, tile));
            var bird = new Player(Animal.Bird);
            Assert.AreEqual(2, bird.DominationScoreOn(g.map, tile));
            
            var dominator = g.DominatedBy(tile);
            Assert.AreEqual(null, dominator);
        }
        
        [Test()]
        public void TestAheadButNoPresence ()
        {
            // Add a grub and two seeds and an arachnid but no bird
            chits.First().Element = Chit.ElementType.Grub;
            chits.ElementAt(1).Element = Chit.ElementType.Seed;
            chits.ElementAt(2).Element = Chit.ElementType.Seed;
            tile.Species[(int)Animal.Arachnid] = 1;
            
            var arachnid = new Player(Animal.Arachnid);
            Assert.AreEqual(2, arachnid.DominationScoreOn(g.map, tile));
            var bird = new Player(Animal.Bird);
            Assert.AreEqual(4, bird.DominationScoreOn(g.map, tile));

            var dominator = g.DominatedBy(tile);
            Assert.AreEqual(Animal.Arachnid, dominator.Animal);
        }
    }
}