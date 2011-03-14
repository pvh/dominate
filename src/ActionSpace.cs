using System;
namespace DominantSpecies
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
    Domination
  }
    
  public class ActionSpace {
    public ActionType Type;
    public Player Player { get; set; }
    
    public ActionSpace(ActionType t) {
      Type = t;
    }
  }
  
  public class InitiativeActionSpace : ActionSpace
  {
    public InitiativeActionSpace() : base (ActionType.Initiative) {}
  }
  
  public class AdaptationActionSpace : ActionSpace
  {
    public AdaptationActionSpace() : base (ActionType.Adaptation) {}
  }
  
  public class RegressionActionSpace : ActionSpace
  {
    public RegressionActionSpace() : base (ActionType.Regression) {}
  }
  
  public class AbundanceActionSpace : ActionSpace
  {
    public AbundanceActionSpace() : base (ActionType.Abundance) {}
  }
  
  public class WastelandActionSpace : ActionSpace
  {
    public WastelandActionSpace() : base (ActionType.Wasteland) {}
  }
  
  public class DepletionActionSpace : ActionSpace
  {
    public DepletionActionSpace() : base (ActionType.Depletion) {}
  }
  
  public class GlaciationActionSpace : ActionSpace
  {
    public GlaciationActionSpace() : base (ActionType.Glaciation) {}
  }
  
  public class SpeciationActionSpace : ActionSpace 
  {
    public Chit.ElementType Element;
    public SpeciationActionSpace(Chit.ElementType element) : base (ActionType.Speciation) {
      Element = element;
    }
  }
  
  public class WanderlustActionSpace : ActionSpace
  {
    public WanderlustActionSpace() : base (ActionType.Wanderlust) {}
  }
  
  public class MigrationActionSpace : ActionSpace
  {
    public int Count;
    public MigrationActionSpace(int count) : base (ActionType.Migration) {
      Count = count;
    }
  }
  
  public class CompetitionActionSpace : ActionSpace
  {
    public Tile.TerrainType[] Terrains;
    public CompetitionActionSpace(Tile.TerrainType[] terrains) : base (ActionType.Competition)
    {
      Terrains = terrains;
    }
  }
  
  public class DominationActionSpace : ActionSpace
  {
    public DominationActionSpace() : base (ActionType.Domination) {}
  }
}


