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
            Activity activity = GetNextActivity();
            
            Assert.IsInstanceOfType(typeof(AbundanceActivity), activity);
        }
    }
}