using System;
using NUnit.Framework;
using NSubstitute;

using DominantSpecies;

namespace Tests
{
    [TestFixture()]
    public class ActivityTests : GameControllerTests
    {
        [Test()]
        public void AbundanceActivity ()
        {
            AddActionPawnFor(g.Players[0], ActivityType.Abundance);
            
            AbundanceActivity activity = GetNextActivity<AbundanceActivity>();
            
            Assert.IsInstanceOfType(typeof(AbundanceActivity), activity);
            
            activity.SelectedChit = activity.ValidChits[0];
            activity.SelectedElementType = activity.ValidTypes[0];
            
            g.Received().PlaceChit(activity.SelectedChit, activity.SelectedElementType);
            
            g.ResolveActivity(activity);
            
            Assert.AreEqual(activity.SelectedChit.Element, activity.SelectedElementType);
        }
        
        [Test()]
        public void SpeciationActivity()
        {
            AddActionPawnFor(g.Players[0], ActivityType.Speciation);
            
            SpeciationActivity activity = GetNextActivity<SpeciationActivity>();
            
            Assert.IsInstanceOfType(typeof(SpeciationActivity), activity);
            
            activity.SelectedLocation = activity.SelectableLocations[0];
            
            Tile t1 = new Tile { Terrain = Tile.TerrainType.Desert };
            Tile t2 = new Tile { Terrain = Tile.TerrainType.Sea };
            Tile t3 = new Tile { Terrain = Tile.TerrainType.Jungle, Tundra = true };
            
            g.TilesFor(activity.SelectedLocation).Returns(new Tile[] { t1, t2, t3 });
            
            g.ResolveActivity(activity);
            
            Assert.AreEqual(2, t1.Species[(int)g.Players[0].Animal]);
            Assert.AreEqual(4, t2.Species[(int)g.Players[0].Animal]);
            Assert.AreEqual(1, t3.Species[(int)g.Players[0].Animal]);
        }
    }
}