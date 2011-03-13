using System;
using NUnit.Framework;

using DominantSpecies;

namespace Tests
{
    [TestFixture()]
    public class ActivityTests : GameControllerTests
    {
        [Test()]
        public void AbundanceActivity ()
        {
            
            
            AbundanceActivity activity = GetNextActivity<AbundanceActivity>();
            
            Assert.IsInstanceOfType(typeof(AbundanceActivity), activity);
            
            activity.SelectedChit = activity.ValidChits[0];
            activity.SelectedElementType = activity.ValidTypes[0];
            
            g.ResolveActivity(activity);
            
            Assert.AreEqual(activity.SelectedChit.Element, activity.SelectedElementType);
        }
    }
}