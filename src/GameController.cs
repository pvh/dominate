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
      foreach (Activity a in g.ActionDisplay.GetActivities())
      {
        a.GC = this;
        
        yield return a;
      }
    }
    
    public bool ResolveActivity(Activity activity)
    {
      if (activity.IsValid)
      {
        activity.Do();
      
        return true;
      }
      
      return false;
    }
    
    internal void PlaceChit(Chit chit, Chit.ElementType elementType)
    {
      chit.Element = elementType;
    }
    
    internal void RemoveChit(Chit chit)
    {
      chit.Element = Chit.ElementType.None;
    }
    
    internal void PlaceActionPawn(Player player, ActivityType activityType)
    {
      g.ActionDisplay.PlaceActionPawn(player, activityType);
    }
  }
}