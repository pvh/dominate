using System;
using System.Collections.Generic;
using System.Linq;

using DominantSpecies.Activities;

namespace DominantSpecies
{
  public class ActionPhase
  {
    public ActionPhase ()
    {
    }
    
    public IEnumerable<Activity> GetActivities(Game g)
    {
      var actionSpaces = g.ActionDisplay.ActionSpaces;
      
      // Initiative
      if (actionSpaces[ActionType.Initiative][0].Player != null) {
        yield return new DummyActivity(ActivityType.InitiativeSpace);
      }
    
      // Action phase activities related to the player's adaptation scale.
      foreach (AdaptationActionSpace a in actionSpaces[ActionType.Adaptation])
      {
        if (a.Player == null) continue;
        
        // FIXME: hardcoded
        Chit.ElementType[] validElements = new Chit.ElementType[] { Chit.ElementType.Grass };
            
        yield return new AdaptationActivity(a.Player, new List<Chit.ElementType>(validElements));
      }
      
      foreach (RegressionActionSpace a in actionSpaces[ActionType.Regression])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.RegressionSpace);
      }
      
      if (g.PlayerFor(Animal.Reptile) != null) {
        yield return new DummyActivity(ActivityType.RegressionSpace);
      }
      
      yield return new DummyActivity(ActivityType.RegressionExecution);
      
      // Board chit placement / removal
      foreach (AbundanceActionSpace a in actionSpaces[ActionType.Abundance])
      {
        if (a.Player == null) continue;
        
        // hardcoded
        Chit.ElementType[] validElementTypes = new Chit.ElementType[] { Chit.ElementType.Grass };
        
        // This is wrong, as it should be chits only on placed tiles, but it works.
        Chit[] validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToArray();
        
        yield return new AbundanceActivity(a.Player, validElementTypes, validChitLocations);
      }
      
      foreach (WastelandActionSpace a in actionSpaces[ActionType.Wasteland])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.WastelandSpace);
      }
      
      yield return new DummyActivity(ActivityType.WastelandExecution);
      
      foreach (DepletionActionSpace a in actionSpaces[ActionType.Depletion])
      {
        if (a.Player == null) continue;
        // hardcoded
        Chit.ElementType[] depletionValidElementTypes = new Chit.ElementType[] { Chit.ElementType.Grass };
        // Find all chits that are valid to be removed
        Chit[] depletionValidChitLocations = g.map.Chits.All.FindAll(chit => depletionValidElementTypes.Contains(chit.Element)).ToArray();
        
        yield return new DepletionActivity(a.Player, depletionValidElementTypes, depletionValidChitLocations);
      }
      
      // Glaciation
      var activeGlaciationActionSpace = actionSpaces[ActionType.Glaciation][0];
      if (activeGlaciationActionSpace.Player != null) {
        // find all tundra tiles
        List<Tile> tundraTiles = g.map.Tiles.All.FindAll(tile => tile.Tundra);
        
        HashSet<Tile> eligibleTiles = new HashSet<Tile>();
        tundraTiles.ForEach(delegate(Tile tile)
                            {
          eligibleTiles.UnionWith(g.map.AdjacentTiles(tile));
        });
        
        yield return new GlaciationActivity(activeGlaciationActionSpace.Player, tundraTiles);
      }
      
      // Speciation
      foreach (SpeciationActionSpace a in actionSpaces[ActionType.Speciation])
      {
        if (a.Player == null) continue;
        
        // hardcoded
        Chit.ElementType selectedElement = Chit.ElementType.Grass;
        List<Chit> selectableLocations = g.map.Chits.All.FindAll(chit => chit.Element == selectedElement);
        
        yield return new SpeciationActivity(a.Player, selectableLocations);
      }
      
      // Special speciation for the insect player
      if (g.PlayerFor(Animal.Insect) != null) {
        yield return new DummyActivity(ActivityType.SpeciationSpace);
      }
      
      // Wanderlust (tile placement)
      foreach (WanderlustActionSpace a in actionSpaces[ActionType.Wanderlust])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.WanderlustSpace);
      }
      
      // Migration (move species cubes)
      foreach (MigrationActionSpace a in actionSpaces[ActionType.Migration])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.MigrationSpace);
      }
      
      // Special competition for the arachnid player
      if (g.PlayerFor(Animal.Arachnid) != null) {
        yield return new DummyActivity(ActivityType.CompetitionSpace);
      }
      
      // Competition (remove other player's cubes)
      foreach (CompetitionActionSpace a in actionSpaces[ActionType.Competition])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.CompetitionSpace);
      }
      
      // Domination. Scoring!
      foreach (DominationActionSpace a in actionSpaces[ActionType.Domination])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.DominationSpace);
      }
      
    }
  }
}

