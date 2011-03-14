using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  public class ActionPhase
  {
    public ActionPhase ()
    {
    }
    
    public IEnumerable<Activity> GetActivities(Game g)
    {
      foreach (KeyValuePair<ActionDisplay.ActionType, List<Player>> actionStep in actions)
      {
        foreach (Player player in actionStep.Value)
        {
          switch (actionStep.Key)
          {
          case ActionDisplay.ActionType.Abundance:
            // hardcoded
            Chit.ElementType[] validElementTypes = new Chit.ElementType[] { Chit.ElementType.Grass };
            
            // This is wrong, as it should be chits only on placed tiles, but it works.
            Chit[] validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToArray();
            
            yield return new AbundanceActivity(player, validElementTypes, validChitLocations);
            break;
          case ActionDisplay.ActionType.Speciation:
            // hardcoded
            Chit.ElementType selectedElement = Chit.ElementType.Grass;
            
            List<Chit> selectableLocations = g.map.Chits.All.FindAll(chit => chit.Element == selectedElement);
            
            yield return new SpeciationActivity(player, selectableLocations);
            break;
          }
        }
      }
    }
  }
}

