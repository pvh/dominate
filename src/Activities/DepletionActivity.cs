using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class DepletionActivity : PlayerActivity
  {
    public List<Chit> DepletionChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public DepletionActivity(Player player, List<Chit> depletionChits, Map map) : base (player)
    {
      DepletionChits = depletionChits;
      
      SelectedElementType = Chit.ElementType.None;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Depletion; }
    }
    
    public override bool IsValid
    {
      get
      {
        return !(SelectedChit == null ||
                 SelectedElementType == Chit.ElementType.None ||
                 !DepletionChits.Contains(SelectedChit));
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