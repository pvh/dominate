using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  /* Class is called ActionDisplay because thats what the game calls the Action area. No display done here */
  public class ActionDisplay
  {
    private Game g;
    
    private enum ActionType
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
    
    private SortedDictionary<ActionType, List<Player>> actions = new SortedDictionary<ActionType, List<Player>>();
    
    public ActionDisplay (Game game)
    {
      this.g = game;
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
    
    public IEnumerable<Activity> GetActivities()
    {
      foreach (KeyValuePair<ActionType, List<Player>> actionStep in actions)
      {
        foreach (Player player in actionStep.Value)
        {
          switch (actionStep.Key)
          {
          case ActionType.Abundance:
            // hardcoded
            Chit.ElementType[] validElementTypes = new Chit.ElementType[] { Chit.ElementType.Grass };
            
            // This is wrong, as it should be chits only on placed tiles, but it works.
            Chit[] validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToArray();
            
            yield return new AbundanceActivity(player, validElementTypes, validChitLocations);
            break;
          case ActionType.Speciation:
            // hardcoded
            Chit.ElementType selectedElement = Chit.ElementType.Grass;
            
            List<Chit> selectableLocations = g.map.Chits.All.FindAll(chit => chit.Element == selectedElement);
            
            yield return new SpeciationActivity(player, selectableLocations);
            break;
          }
        }
      }
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

