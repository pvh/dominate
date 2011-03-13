using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies
{
  public class GameController
  {
    private Game g;
    
    public GameController ()
    {
      g = new Game();
    }
    
    public IEnumerable<Activity> GetActivities()
    {
      yield return new AbundanceActivity(new Chit.ElementType[] { Chit.ElementType.Grass }, g.map.ChitsFor(1, 1));
    }
  }
}