using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class AdaptationActivity : PlayerActivity
  {
    public List<Chit> ValidChits { get; private set; }
    
    public Chit.ElementType SelectedElement { get; set; }
    
    public AdaptationActivity(Player player, List<Chit> validChits) : base (player)
    {
      ValidChits = validChits;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Adaptation; }
    }
    
    public override bool IsValid
    {
      get
      {
        return true;
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.AddElementToPlayer(Player, SelectedElement);
    }
    
    public override void Undo(GameController GC)
    {
      throw new NotImplementedException();
    }
  }
  
}