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
            Assert.AreEqual(Species.Arachnid, g.DominatedBy(tile).Species);
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