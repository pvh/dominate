using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies
{
  public class GameController
  {
    protected Game g;
    
    public GameController()
    {
      g = new Game();
    }
    
    public IEnumerable<Activity> GetActivities()
    {
      ActionPhase actionPhase = new ActionPhase();
      foreach (Activity a in actionPhase.GetActivities(g.ActionDisplay.Actions, g))
      {
        yield return a;
      }
    }
    
    public bool ResolveActivity(Activity activity)
    {
      if (activity.IsValid)
      {
        activity.Do(this);
      
        return true;
      }
      
      return false;
    }
    
    public List<Player> Players
    {
      get { return g.Players; }
    }
    
    public void PlaceChit(Chit chit, Chit.ElementType elementType)
    {
      chit.Element = elementType;
    }
    
    public void RemoveChit(Chit chit)
    {
      chit.Element = Chit.ElementType.None;
    }
    
    public void PlaceActionPawn(Player player, ActionDisplay.ActionType actionType)
    {
      g.ActionDisplay.PlaceActionPawn(player, actionType);
    }
    
    public virtual Tile[] TilesFor(Chit c)
    {
      return g.map.TilesFor(c);
    }
    
    public void AddElementToPlayer(Player player, Chit.ElementType element)
    {
      player.Adapt(element);
    }
  }
}