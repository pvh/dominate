using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class RegressionActivity : PlayerActivity
  {
    public RegressionActivity(Player player) : base (player)
    {
      throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.Regression; }
    }
    
    public override bool IsValid {
      get {
        throw new NotImplementedException ();
      }
    }
    
    public override void Do (GameController GC)
    {
      throw new NotImplementedException ();
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}