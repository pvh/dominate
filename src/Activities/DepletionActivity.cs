using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class DepletionActivity : PlayerActivity
  {
    public List<Chit> AvailableChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    
    public DepletionActivity(Player player, List<Chit> depletionChits) : base (player)
    {
      AvailableChits = depletionChits;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Depletion; }
    }
    
    public override bool IsValid
    {
      get
      {
        return !(SelectedChit == null || !AvailableChits.Contains(SelectedChit));
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.RemoveChit(SelectedChit);
    }
    
    public override void Undo(GameController GC)
    {
      throw new NotImplementedException();
    }
  }
}