using System;
using NUnit.Framework;

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
}