using System;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

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
            
            public PlaceActionPawnActivity GetPlaceActionPawnActivity(Player p)
            {
                List<ActionSpace> availableActionSpaces = g.ActionDisplay.AvailableActionSpaces;
                
                return new PlaceActionPawnActivity(p, availableActionSpaces);
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
                if (activity is T)
                {
                    return (T)activity;
                }
            }
            
            return null;
        }
        
        public void AddActionPawnFor(Player p, ActionType a)
        {
            PlaceActionPawnActivity act = g.GetPlaceActionPawnActivity(p);
            act.SelectedAction = act.ValidActionSpaces.Find(space => space.Type == a);
            g.ResolveActivity(act);
        }
    }
}

