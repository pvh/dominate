using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class AbundanceActivity : PlayerActivity
  {
    public List<Chit> AvailableChits { get; private set; }
    public List<Chit> ValidChitLocations { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public AbundanceActivity(Player player, List<Chit> availableChits, List<Chit> validChitLocations) : base (player)
    {
      AvailableChits = availableChits;
      ValidChitLocations = validChitLocations;
        
      SelectedElementType = Chit.ElementType.None;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Abundance; }
    }
    
    public override bool IsValid
    {
      get
      {
        if (SelectedChit == null || SelectedElementType == Chit.ElementType.None)
        {
          return false;
        }
        
        return true;
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.PlaceChit(SelectedChit, SelectedElementType);
    }
    
    public override void Undo(GameController GC)
    {
      GC.RemoveChit(SelectedChit);
    }
  }
}