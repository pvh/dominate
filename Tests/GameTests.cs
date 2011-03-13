using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

using DominantSpecies;

namespace Tests
{
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
    public class GameScoringTests : GameTests
    {
        [Test()]
        public void TestSimpleScoring ()
        {
            tile.Terrain = Tile.TerrainType.Sea;
            tile.Species[(int) Animal.Amphibian] = 5;
            tile.Species[(int) Animal.Insect] = 4;
            tile.Species[(int) Animal.Arachnid] = 3;
            tile.Species[(int) Animal.Mammal] = 2;
            
            var scores = g.ScoreFor(tile);
            
            Assert.AreEqual(9, scores[g.PlayerFor(Animal.Amphibian)]);
            Assert.AreEqual(5, scores[g.PlayerFor(Animal.Insect)]);
            Assert.IsFalse(scores.ContainsKey(g.PlayerFor(Animal.Reptile)));
        }
        
        [Test()]
        public void TestTiesBreakByFoodChain ()
        {
            tile.Terrain = Tile.TerrainType.Sea;
            tile.Species[(int) Animal.Amphibian] = 5;
            tile.Species[(int) Animal.Insect] = 5;
            
            var scores = g.ScoreFor(tile);
            
            Assert.AreEqual(9, scores[g.PlayerFor(Animal.Amphibian)]);
            Assert.AreEqual(5, scores[g.PlayerFor(Animal.Insect)]);
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
            Assert.AreEqual(2, g.PlayerFor(Animal.Arachnid).DominationScoreOn(g.map, tile));
            Assert.AreEqual(2, g.PlayerFor(Animal.Bird).DominationScoreOn(g.map, tile));
            
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
            
            Assert.AreEqual(2, g.PlayerFor(Animal.Arachnid).DominationScoreOn(g.map, tile));
            Assert.AreEqual(4, g.PlayerFor(Animal.Bird).DominationScoreOn(g.map, tile));
            
            var dominator = g.DominatedBy(tile);
            Assert.AreEqual(Animal.Arachnid, dominator.Animal);
        }
    }
}