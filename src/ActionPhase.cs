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
        yield return new InitiativeActivity(g.Players);
      }
    
      // Action phase activities related to the player's adaptation scale.
      foreach (AdaptationActionSpace a in actionSpaces[ActionType.Adaptation])
      {
        if (a.Player == null) continue;
        yield return new AdaptationActivity(a.Player, g.ActionDisplay.AdaptationChits);
      }
      
      var regressionProtection = new Dictionary<Player, Chit.ElementType> {};
      foreach (RegressionActionSpace a in actionSpaces[ActionType.Regression])
      {
        if (a.Player == null) continue;
        yield return new RegressionActivity(a.Player, g.ActionDisplay.RegressionChits);
      }
      
      if (g.PlayerFor(Animal.Reptile) != null) {
        yield return new RegressionActivity(g.PlayerFor(Animal.Reptile), g.ActionDisplay.RegressionChits);
      }
      
      // TODO: model protection from regression somehow.
      yield return new RegressionExecutionActivity(g.ActionDisplay.RegressionChits, regressionProtection);
      
      // Board chit placement / removal
      foreach (AbundanceActionSpace a in actionSpaces[ActionType.Abundance])
      {
        if (a.Player == null) continue;
        
        List<Chit> validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToList();
        
        yield return new AbundanceActivity(a.Player, g.ActionDisplay.AbundanceChits, validChitLocations);
      }
      
      foreach (WastelandActionSpace a in actionSpaces[ActionType.Wasteland])
      {
        if (a.Player == null) continue;
        yield return new WastelandActivity(a.Player, g.ActionDisplay.WastelandChits);
      }
      
      yield return new WastelandExecutionActivity(g.map, g.ActionDisplay.WastelandChits);
      
      foreach (DepletionActionSpace a in actionSpaces[ActionType.Depletion])
      {
        if (a.Player == null) continue;
        
        List<Chit.ElementType> validElementTypesForRemoval = g.ActionDisplay.DepletionChits.ConvertAll(chit => chit.Element);
        
        List<Chit> validChitsForRemoval = g.map.Chits.All.FindAll(chit => validElementTypesForRemoval.Contains(chit.Element));
        
        yield return new DepletionActivity(a.Player, validChitsForRemoval);
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
        
        // XXX FIXME: hardcoded
        Chit.ElementType selectedElement = Chit.ElementType.Grass;
        List<Chit> selectableLocations = g.map.Chits.All.FindAll(chit => chit.Element == selectedElement);
        
        yield return new SpeciationActivity(a.Player, selectableLocations);
      }
      
      // Special speciation for the insect player
      var insect = g.PlayerFor(Animal.Insect);
      if (insect != null) {
        // XXX FIXME: hardcoded
        Chit.ElementType selectedElement = Chit.ElementType.Grass;
        List<Chit> selectableLocations = g.map.Chits.All.FindAll(chit => chit.Element == selectedElement);
        
        yield return new SpeciationActivity(insect, selectableLocations);
      }
      
      // Wanderlust (tile placement)
      foreach (WanderlustActionSpace a in actionSpaces[ActionType.Wanderlust])
      {
        if (a.Player == null) continue;
        yield return new WanderlustActivity(a.Player, g.map, new List<Tile> {});
      }
      
      // Migration (move species cubes)
      foreach (MigrationActionSpace a in actionSpaces[ActionType.Migration])
      {
        if (a.Player == null) continue;
        // XXX FIXME: hardcoded
        int count = 7;
        List<Tile> locations = g.map.Tiles.All.FindAll(tile => 
                                                       tile.Species[(int) a.Player.Animal] > 0);
        yield return new MigrationActivity(a.Player, count, locations);
      }
      
      // Special competition for the arachnid player
      var arachnid = g.PlayerFor(Animal.Arachnid);
      if (g.PlayerFor(Animal.Arachnid) != null) {
        // XXX FIXME: can only compete on tiles where another player is, really
        List<Tile> locations = g.map.Tiles.All.FindAll(tile => {
          return tile.Species[(int) arachnid.Animal] > 0;
        });
        yield return new CompetitionActivity(arachnid, locations);
      }
      
      // Competition (remove other player's cubes)
      foreach (CompetitionActionSpace a in actionSpaces[ActionType.Competition])
      {
        if (a.Player == null) continue;
        // XXX FIXME: can only compete on tiles where another player is, really
        List<Tile> locations = g.map.Tiles.All.FindAll(tile => {
          return tile.Species[(int) a.Player.Animal] > 0;
        });
        yield return new CompetitionActivity(a.Player, locations);
      }
      
      // Domination. Scoring!
      foreach (DominationActionSpace a in actionSpaces[ActionType.Domination])
      {
        if (a.Player == null) continue;
        yield return new DominationActivity(a.Player, g.map.Tiles.All);
      }
    }
  }
}
