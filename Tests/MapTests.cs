using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

using DominantSpecies;
using System.Linq;

namespace Tests
{
    [TestFixture()]
    public class GameTests
    {
        [Test()]
        public void TestDominatedBy ()
        {
            // no default setup
            Game g = new Game(false);
            var tile = g.map.tiles[3, 3];
            var chits = g.map.ChitsFor(tile);
            
            // Be sure we're dealing with a tile that has nothing configured on it
            Assert.IsTrue(chits.All(chit => 
                                    (chit.Element == Chit.ElementType.None
                                     || 
                                     chit.Element == Chit.ElementType.Invalid)));
            Assert.AreEqual(Tile.TerrainType.Empty, tile.Terrain);
            
            // Add a grub
            chits.First().Element = Chit.ElementType.Grub;
            
            // A default spider should now dominate
            var dominator = g.DominatedBy(tile);
            Assert.AreNotEqual(null, dominator);
            
            Assert.AreEqual(Species.Arachnid, dominator.Species);
        }
        
        [Test()]
        public void TestTiedForDomination ()
        {
            // no default setup
            Game g = new Game(false);
            var tile = g.map.tiles[3, 3];
            var chits = g.map.ChitsFor(tile);
            
            // Be sure we're dealing with a tile that has nothing configured on it
            Assert.IsTrue(chits.All(chit => 
                                    (chit.Element == Chit.ElementType.None
                                     || 
                                     chit.Element == Chit.ElementType.Invalid)));
            Assert.AreEqual(Tile.TerrainType.Empty, tile.Terrain);
            
            // Add a grub
            chits.First().Element = Chit.ElementType.Grub;
            chits.ElementAt(1).Element = Chit.ElementType.Seed;
            
            var arachnid = new Player(Species.Arachnid);
            Assert.AreEqual(2, arachnid.DominationScoreOn(g.map, tile));
            var bird = new Player(Species.Bird);
            Assert.AreEqual(2, bird.DominationScoreOn(g.map, tile));
            
            var dominator = g.DominatedBy(tile);
            Assert.AreEqual(null, dominator);
        }
    }
    
    [TestFixture()]
    public class PlayerTests
    {
        [Test()]
        public void TestSimpleDominanceScore ()
        {
            Map m = new Map();
            Player p = new Player(Species.Insect);
            
            // Create a known dominance
            var tile = m.tiles[3,3];
            tile.Terrain = Tile.TerrainType.Mountain;
            var chits = m.ChitsFor(tile);
            chits[0].Element = Chit.ElementType.Grass;
            tile.Species[(int) Species.Insect] = 1;
            
            // Dominance should be 2
            Assert.AreEqual(2, p.DominationScoreOn(m, tile));
        }
    }
    
	[TestFixture()]
	public class MapTests
	{
		[Test()]
		public void TestTileIsInvalid ()
		{
		    Game g = new Game();
            
            Assert.AreEqual(Tile.TerrainType.Invalid, g.map.Tiles[0, 0].Terrain);
		}
	}
    
    [TestFixture()]
    public class ChitTests
    {
        [Test()]
        public void TestNeighboringChitsForTile()
        {
            Game g = new Game();
            
            var chits = new Chit.ElementType[] {
                Chit.ElementType.Grass,
                Chit.ElementType.Grub,
                Chit.ElementType.Meat,
                Chit.ElementType.Seed,
                Chit.ElementType.Sun,
                Chit.ElementType.Water};
            
            var tile = g.map.tiles[3, 3];
            Assert.AreEqual( Tile.TerrainType.Sea, tile.Terrain );
            CollectionAssert.AreEquivalent( chits, g.map.ChitsFor(tile).Select(chit => chit.Element) );
        }
        
        [Test()] [Ignore("TilesFor is blatantly and completely wrong")]
        public void TestNeighboringTilesForChit()
        {
            Game g = new Game();
            
            var tiles = new Tile.TerrainType[] {
                Tile.TerrainType.Sea,
                Tile.TerrainType.Wetlands,
                Tile.TerrainType.Savannah};
            var chit = g.map.chits[4,5];
            Assert.AreEqual(Chit.ElementType.Water, chit.Element);
            CollectionAssert.AreEquivalent( tiles, g.map.TilesFor(chit).Select(tile => tile.Terrain).ToList() );
        }
    }
}