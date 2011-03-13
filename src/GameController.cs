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
      var a = new AbundanceActivity(new Chit.ElementType[] { Chit.ElementType.Grass }, g.map.ChitsFor(g.map.Tiles[1, 1]));
      a.GC = this;
      
      yield return a;
    }
    
    public bool ResolveActivity(Activity activity)
    {
      return true;
    }
    
    internal void PlaceChit(Chit chit, Chit.ElementType elementType)
    {
      chit.Element = elementType;
    }
    
    internal void RemoveChit(Chit chit)
    {
      chit.Element = Chit.ElementType.None;
    }
  }
}