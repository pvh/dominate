using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  /* Class is called ActionDisplay because thats what the game calls the Action area. No display done here */
  public class ActionDisplay
  {
    public enum ActionType
    {
      Invalid,
      Initiative,
      Adaptation,
      Regression,
      Abundance,
      Wasteland,
      Depletion,
      Glaciation,
      Speciation,
      Wanderlust,
      Migration,
      Competition,
      Dominance
    }
    
    public SortedDictionary<ActionType, List<Player>> Actions { get; set; }
    
    public ActionDisplay ()
    {
      Actions = new SortedDictionary<ActionType, List<Player>>();
    }
    
    /* this is broken, because you are placing in an actual location, not on the type */
    public void PlaceActionPawn(Player player, ActionType type)
    {
      if (!Actions.ContainsKey(type))
      {
        Actions[type] = new List<Player>();
      }
      
      Actions[type].Add(player);
    }
  }
}

