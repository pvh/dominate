
namespace DominantSpecies.Activities
{
  public enum ActivityType
  {
    /* Activity types for actions on the ActionDisplay */
    InitiativeSpace,
    AdaptationSpace,
    RegressionSpace,
    RegressionExecution,
    AbundanceSpace,
    WastelandSpace,
    WastelandExecution,
    DepletionSpace,
    DepletionExecution,
    GlaciationSpace,
    SpeciationSpace,
    WanderlustSpace,
    MigrationSpace,
    CompetitionSpace,
    DominationSpace,
    
    // not included: ReptileRegression, InspectSpeciation, SpiderCompetition
    // also not included: EndOfTurn activities or intermediating PhaseBookendActivities?
    
    // Backwards Compatibility
    Adaptation,
    Abundance,
    Speciation,
    Regression,
    Glaciation,
    
    /* Planning phase activities */
    PlaceActionPawn
  }
  
  public abstract class Activity
  {
    public Activity ()
    {
    }
    
    public abstract bool IsValid { get; }
    
    public abstract void Do(GameController GC);
    
    public abstract void Undo(GameController GC);
    
    public abstract ActivityType Type { get; }
  }
  
  public abstract class PlayerActivity : Activity
  {
    public Player Player { get; private set; }
    
    public PlayerActivity(Player player)
    {
      Player = player;
    }
  }
  
  public class DummyActivity : Activity
  {
    ActivityType myType;
    public DummyActivity(ActivityType t) {
      myType = t;
    }
    public override bool IsValid { get { return true; } }
    public override void Do(GameController gc) {}
    public override void Undo(GameController gc) {}
    public override ActivityType Type { get
      { return myType; }
    }
  }
}