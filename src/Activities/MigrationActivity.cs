using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class MigrationActivity : PlayerActivity
  {
    public MigrationActivity(Player player, int count, List<Tile> tiles) : base (player)
    {
      //throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.MigrationSpace; }
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