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
      /*foreach (KeyValuePair<ActionDisplay.ActionType, List<Player>> actionStep in actions)
      {
        foreach (Player player in actionStep.Value)
        {
          switch (actionStep.Key)
          {
          case ActionDisplay.ActionType.Adaptation:
            // hardcoded
            Chit.ElementType[] validElements = new Chit.ElementType[] { Chit.ElementType.Grass };
            
            yield return new AdaptationActivity(player, new List<Chit.ElementType>(validElements));
            break;
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
          case ActionDisplay.ActionType.Glaciation:
            // find all tundra tiles
            List<Tile> tundraTiles = g.map.Tiles.All.FindAll(tile => tile.Tundra);
            
            HashSet<Tile> eligibleTiles = new HashSet<Tile>();
            tundraTiles.ForEach(delegate(Tile tile)
            {
              eligibleTiles.UnionWith(g.map.AdjacentTiles(tile));
            });
            
            yield return new GlaciationActivity(player, tundraTiles);
            break;
          default:
            throw new NotImplementedException(String.Format("Have not implemented ActionType {0} yet", Enum.GetName(typeof(ActionDisplay.ActionType), actionStep.Key)));
          }
        }*/
      var actionSpaces = g.ActionDisplay.ActionSpaces;
      
      // Initiative
      if (actionSpaces[ActionDisplay.ActionType.Initiative][0].Player != null) {
        yield return new DummyActivity(ActivityType.InitiativeSpace);
      }
    
      // Action phase activities related to the player's adaptation scale.
      foreach (ActionDisplay.AdaptationActionSpace a in actionSpaces[ActionDisplay.ActionType.Adaptation])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.AdaptationSpace);
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
        yield return new DummyActivity(ActivityType.AbundanceSpace);
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
        yield return new DummyActivity(ActivityType.GlaciationSpace);
      }
      
      // Speciation
      foreach (ActionDisplay.SpeciationActionSpace a in actionSpaces[ActionDisplay.ActionType.Speciation])
      {
        if (a.Player == null) continue;
        yield return new DummyActivity(ActivityType.SpeciationSpace);
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

