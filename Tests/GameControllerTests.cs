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
        
        public T GetNextActivity<T>() where T : Activity
        {
            foreach (Activity activity in g.GetActivities())
            {
                return (T)activity;
            }
            
            return null;
        }
    }
}

