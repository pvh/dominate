using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class WastelandActivity : PlayerActivity
  {
    public WastelandActivity(Player player, List<Chit> validChits) : base (player)
    {
      //throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.WastelandSpace; }
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
  
  public class WastelandExecutionActivity : Activity
  {
    public WastelandExecutionActivity(Map map, List<Chit> wastelandChits)
    {
      //throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.WastelandExecution; }
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