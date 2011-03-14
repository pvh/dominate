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
        
        [Test]
        public void TestStartingNumberOfPlayersIsValid()
        {
            try
            {
                new Player(Animal.Amphibian, 1);
                Assert.Fail("Number of players was allowed to be set too low");
            }
            catch (Exception) {}
            
            try
            {
                new Player(Animal.Amphibian, 7);
                Assert.Fail("Number of players was allowed to be set too high");
            }
            catch (Exception) {}
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
        
        [Test]
        public void TestCanAdapt()
        {
            GameController gc = new GameController();
            Player p = gc.Players[0];
            
            int beginCount = 0;
            foreach(Chit.ElementType element in Enum.GetValues(typeof(Chit.ElementType)))
                beginCount += p.AdaptationTo(element);
                
            gc.AddElementToPlayer(p, Chit.ElementType.Grass);
            
            int endCount = 0;
            foreach(Chit.ElementType element in Enum.GetValues(typeof(Chit.ElementType)))
                endCount += p.AdaptationTo(element);
            
            Assert.AreEqual(beginCount + 1, endCount);
        }
        
        [Test]
        public void TestCannotAdaptOver6()
        {
            GameController gc = new GameController();
            Player p = gc.Players.Find(delegate(Player pl) { return pl.Animal == Animal.Arachnid; });
            
            // Fill the list
            for(int i = 0; i < 4; i++)
                gc.AddElementToPlayer(p, Chit.ElementType.Grub);
            
            try
            {
                gc.AddElementToPlayer(p, Chit.ElementType.Grub);
                Assert.Fail("Was able to Adapt to more than 6 elements");
            }
            catch (Exception) {}
        }
        
        [Test]
        public void TestAmphibianGetsLessAdaptation()
        {
            GameController gc = new GameController();
            Player p = gc.Players.Find(delegate(Player pl) { return pl.Animal == Animal.Amphibian; });
            
            // Fill the list
            for(int i = 0; i < 3; i++)
                gc.AddElementToPlayer(p, Chit.ElementType.Grub);
            
            try
            {
                gc.AddElementToPlayer(p, Chit.ElementType.Grub);
                Assert.Fail("Was able to Adapt to more than 6 elements");
            }
            catch (Exception) {}
        }
    }
}

