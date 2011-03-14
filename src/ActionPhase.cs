using System;
using System.Collections.Generic;

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
      if (actionSpaces[ActionDisplay.ActionType.Initiative][0].Player != null) {
        yield return new DummyActivity(ActivityType.InitiativeSpace);
      }
    
      // Action phase activities related to the player's adaptation scale.
      foreach (ActionDisplay.AdaptationActionSpace a in actionSpaces[ActionDisplay.ActionType.Adaptation])
      {
        if (a.Player == null) continue;
        
        // FIXME: hardcoded
        Chit.ElementType[] validElements = new Chit.ElementType[] { Chit.ElementType.Grass };
            
        yield return new AdaptationActivity(a.Player, new List<Chit.ElementType>(validElements));
      }
      
      foreach (ActionDisplay.RegressionActionSpace a in actionSpaces[ActionDisplay.ActionType.Regression])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.RegressionSpace);
      }
      
      if (g.PlayerFor(Animal.Reptile) != null) {
        yield return new DummyActivity(ActivityType.RegressionSpace);
      }
      
      yield return new DummyActivity(ActivityType.RegressionExecution);
      
      // Board chit placement / removal
      foreach (ActionDisplay.AbundanceActionSpace a in actionSpaces[ActionDisplay.ActionType.Abundance])
      {
        if (a.Player == null) continue;
        
        // hardcoded
        Chit.ElementType[] validElementTypes = new Chit.ElementType[] { Chit.ElementType.Grass };
        
        // This is wrong, as it should be chits only on placed tiles, but it works.
        Chit[] validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToArray();
        
        yield return new AbundanceActivity(a.Player, validElementTypes, validChitLocations);
        break;
      }
      
      foreach (ActionDisplay.WastelandActionSpace a in actionSpaces[ActionDisplay.ActionType.Wasteland])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.WastelandSpace);
      }
      
      yield return new DummyActivity(ActivityType.WastelandExecution);
      
      foreach (ActionDisplay.DepletionActionSpace a in actionSpaces[ActionDisplay.ActionType.Depletion])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.DepletionSpace);
      }
      
      // Glaciation
      var activeGlaciationActionSpace = actionSpaces[ActionDisplay.ActionType.Glaciation][0];
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
      foreach (ActionDisplay.SpeciationActionSpace a in actionSpaces[ActionDisplay.ActionType.Speciation])
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
      foreach (ActionDisplay.WanderlustActionSpace a in actionSpaces[ActionDisplay.ActionType.Wanderlust])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.WanderlustSpace);
      }
      
      // Migration (move species cubes)
      foreach (ActionDisplay.MigrationActionSpace a in actionSpaces[ActionDisplay.ActionType.Migration])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.MigrationSpace);
      }
      
      // Special competition for the arachnid player
      if (g.PlayerFor(Animal.Arachnid) != null) {
        yield return new DummyActivity(ActivityType.CompetitionSpace);
      }
      
      // Competition (remove other player's cubes)
      foreach (ActionDisplay.CompetitionActionSpace a in actionSpaces[ActionDisplay.ActionType.Competition])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.CompetitionSpace);
      }
      
      // Domination. Scoring!
      foreach (ActionDisplay.DominationActionSpace a in actionSpaces[ActionDisplay.ActionType.Domination])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.DominationSpace);
      }
      
    }
  }
}

