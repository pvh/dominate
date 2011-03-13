using System;
using NUnit.Framework;

using DominantSpecies;

namespace Tests
{
    public class GameControllerTests
    {
        protected GameController g;
        
        public GameControllerTests ()
        {
        }
        
        [SetUp]
        public void SetUp()
        {
            g = new GameController();
        }
        
        public Activity GetNextActivity()
        {
            foreach (Activity activity in g.GetActivities())
            {
                return activity;
            }
            
            return null;
        }
    }
}

