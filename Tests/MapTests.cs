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
        [Test()]
        public void TestDominatedBy ()
        {
            Game g = new Game();
            var tile = g.map.tiles[2, 3];
            Assert.AreEqual(Tile.TerrainType.Jungle, tile.Terrain);
            
            var dominator = g.DominatedBy(tile);
            Assert.AreNotEqual(null, dominator);
            Assert.AreEqual(Species.Arachnid, dominator.Species);
        }
        
        [Test()] [Ignore("Have to cook up an actual example of a tie for domination")]
        public void TestTiedForDomination ()
        {
            Game g = new Game();
            var tile = g.map.tiles[2, 3];
            tile.Species[(int) Species.Insect] = tile.Species[(int) Species.Insect] + 2;
            
            // Turn two None chits into Grass for this test
            var modchip = g.map.ChitsFor(tile).Single(chit => chit.Element == Chit.ElementType.None);
            modchip.Element = Chit.ElementType.Grass;
            
            modchip = g.map.ChitsFor(tile).Single(chit => chit.Element == Chit.ElementType.None);
            modchip.Element = Chit.ElementType.Grass;
            
            Assert.AreEqual(Tile.TerrainType.Jungle, tile.Terrain);
            Assert.AreEqual(g.Players.Find(p => p.Species == Species.Arachnid).DominationScoreOn(g.map, tile),
                            g.Players.Find(p => p.Species == Species.Amphibian).DominationScoreOn(g.map, tile),
                            "The test is badly framed. These species should have the same dominance.");
            Assert.AreNotEqual(Species.Arachnid, g.DominatedBy(tile).Species);
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