
namespace DominantSpecies.Activities
{
  public enum ActivityType
  {
    /* Activity types for actions on the ActionDisplay */
    Adaptation,
    Abundance,
    Speciation,
    Regression,
    Glaciation,
    Depletion,
    
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
}