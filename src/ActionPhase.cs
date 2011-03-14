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

