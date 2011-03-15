using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

using DominantSpecies;

namespace Tests
{
    
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
    
    [TestFixture()]
    public class ChitBagTests
    {
        [Test()]
        public void ChitBagSize()
        {
            ChitBag c = new ChitBag();
            Assert.AreEqual(120, c.ChitsLeft());
        }
        
        [Test()]
        public void DrawRandomChit()
        {
            ChitBag c = new ChitBag();
            var chit = c.DrawChit();
            Assert.AreEqual(119, c.ChitsLeft());
            Assert.IsNotNull(chit);
            Assert.AreNotEqual(Chit.ElementType.Invalid, chit.Element);
            Assert.AreNotEqual(Chit.ElementType.None, chit.Element);
        }
        [Test()]
        public void DrawSpecificChit()
        {
            ChitBag c = new ChitBag();
            var chit = c.DrawChit();
            Assert.AreEqual(119, c.ChitsLeft());
            Assert.IsNotNull(chit);
            Assert.AreNotEqual(Chit.ElementType.Invalid, chit.Element);
            Assert.AreNotEqual(Chit.ElementType.None, chit.Element);
        }
        
        [Test()]
        public void CountContents()
        {
            ChitBag c = new ChitBag();
            List<Chit> chits = new List<Chit> {};
            for (int i = 0; i < 120; i++) {
                chits.Add(c.DrawChit());
            }
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Grass));
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Grub));
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Meat));
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Seed));
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Sun));
            Assert.AreEqual(20, chits.Count(i => i.Element == Chit.ElementType.Water));
            Assert.AreEqual(0, c.ChitsLeft());
        }
        
        // I'm not sure the game even makes this possible, but what the hell
        [Test()]
        public void DrawTooMany()
        {
            ChitBag c = new ChitBag();
            for (int i = 0; i < 120; i++) {
                c.DrawChit();
            }
            Assert.AreEqual(0, c.ChitsLeft());
            
            var chit = c.DrawChit();
            Assert.AreEqual(null, chit);
        }
    }
}