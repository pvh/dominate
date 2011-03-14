using System;
using NUnit.Framework;
using NSubstitute;

using DominantSpecies;
using DominantSpecies.Activities;

namespace Tests
{
    public class GameControllerTests
    {
        public class MockGameController : GameController
        {
            public Game MockGame
            {
                get { return g; }
            }
            
            public MockGameController()
            {
                g = Substitute.For<Game>(true);
            }
        }
        
        protected MockGameController g;
        
        public GameControllerTests ()
        {
        }
        
        [SetUp]
        public void SetUp()
        {
            g = Substitute.For<MockGameController>();
        }
        
        public T GetNextActivity<T>() where T : Activity
        {
            foreach (Activity activity in g.GetActivities())
            {
                return (T)activity;
            }
            
            return null;
        }
        
        public void AddActionPawnFor(Player p, ActionDisplay.ActionType a)
        {
            PlaceActionPawnActivity act = new PlaceActionPawnActivity(p);
            act.SelectedAction = a;
            g.ResolveActivity(act);
        }
    }
}

