using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class InitiativeActivity : Activity
  {
    public InitiativeActivity(List<Player> players)
    {
    }
        
    public override ActivityType Type {
      get { return ActivityType.InitiativeSpace; }
    }
    
    public override bool IsValid
    {
      get { return true; }
    }
    
    public override void Do(GameController GC)
    {
      //GC.PlaceChit(SelectedChit, SelectedElementType);
    }
    
    public override void Undo(GameController GC)
    {
      //GC.RemoveChit(SelectedChit);
    }
  }
}