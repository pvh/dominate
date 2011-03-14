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
    
    public ActionSpace() {}
    public ActionSpace(ActionType t) {
      Type = t;
    }
  }
  
  public class InitiativeActionSpace : ActionSpace {}
  public class AdaptationActionSpace : ActionSpace {}
  public class RegressionActionSpace : ActionSpace {}
  public class AbundanceActionSpace : ActionSpace {}
  public class WastelandActionSpace : ActionSpace {}
  public class DepletionActionSpace : ActionSpace {}
  public class GlaciationActionSpace : ActionSpace {}
  public class DominationActionSpace : ActionSpace {}
  public class WanderlustActionSpace : ActionSpace {}
  
  // Special cases -- spaces which are distinguishable
  public class SpeciationActionSpace : ActionSpace 
  {
    public Chit.ElementType Element;
    public SpeciationActionSpace(Chit.ElementType element) {
      Element = element;
    }
  }
  
  public class MigrationActionSpace : ActionSpace
  {
    public int Count;
    public MigrationActionSpace(int count) {
      Count = count;
    }
  }
  
  public class CompetitionActionSpace : ActionSpace
  {
    public Tile.TerrainType[] Terrains;
    public CompetitionActionSpace(Tile.TerrainType[] terrains)
    {
      Terrains = terrains;
    }
  }
    
}


