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
    
    public SortedDictionary<ActionType, List<Player>> actions = new SortedDictionary<ActionType, List<Player>>();
    
    public ActionDisplay ()
    {
    }
    
    /* this is broken, because you are placing in an actual location, not on the type */
    public void PlaceActionPawn(Player player, ActivityType type)
    {
      ActionType actionType = ConvertActivityToActionType(type);
      
      if (!actions.ContainsKey(actionType))
      {
        actions[actionType] = new List<Player>();
      }
      
      actions[actionType].Add(player);
    }
    
    private ActionType ConvertActivityToActionType(ActivityType aType)
    {
      switch (aType)
      {
      case ActivityType.Abundance:
        return ActionType.Abundance;
      case ActivityType.Speciation:
        return ActionType.Speciation;
      }
      
      return ActionType.Invalid;
    }
  }
}

