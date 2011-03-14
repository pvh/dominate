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
    public void PlaceActionPawn(Player player, ActivityType type)
    {
      ActionType actionType = ConvertActivityToActionType(type);
      
      if (!Actions.ContainsKey(actionType))
      {
        Actions[actionType] = new List<Player>();
      }
      
      Actions[actionType].Add(player);
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

