using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class AdaptationActivity : PlayerActivity
  {
    public List<Chit.ElementType> ValidElements { get; private set; }
    
    public Chit.ElementType SelectedElement { get; set; }
    
    public AdaptationActivity(Player player, List<Chit.ElementType> validElements) : base (player)
    {
      ValidElements = validElements;
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