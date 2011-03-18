using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class WanderlustActivity : PlayerActivity
  {
    public WanderlustActivity(Player player, Map map, List<Tile> tiles) : base (player)
    {
      //throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.WanderlustSpace; }
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