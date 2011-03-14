using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class DepletionActivity : PlayerActivity
  {
    public List<Chit.ElementType> ValidTypes { get; private set; }
    public List<Chit> ValidChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public DepletionActivity(Player player, Chit.ElementType[] validTypes, Chit[] validChits) : this(player,
                                                                                                     new List<Chit.ElementType>(validTypes),
                                                                                                     new List<Chit>(validChits))
    {
    }
    
    public DepletionActivity(Player player, List<Chit.ElementType> validTypes, List<Chit> validChits) : base (player)
    {
      ValidTypes = validTypes;
      ValidChits = validChits;
      
      SelectedElementType = Chit.ElementType.None;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Depletion; }
    }
    
    public override bool IsValid
    {
      get
      {
        if  (SelectedChit == null || SelectedElementType == Chit.ElementType.None || !ValidChits.Contains(SelectedChit))
        {
          return false;
        }
        
        return true;
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.RemoveChit(SelectedChit);
    }
    
    public override void Undo(GameController GC)
    {
      GC.PlaceChit(SelectedChit, SelectedElementType);
    }
  }
}