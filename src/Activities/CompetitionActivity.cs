using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class CompetitionActivity : PlayerActivity
  {
    public CompetitionActivity(Player player, List<Tile> tiles) : base (player)
    {
      //throw new NotImplementedException();
    }
    
    public override ActivityType Type {
      get { return ActivityType.CompetitionSpace; }
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