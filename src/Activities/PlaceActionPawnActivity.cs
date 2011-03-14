using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class PlaceActionPawnActivity : PlayerActivity
  {
    public List<ActionSpace> ValidActionSpaces { get; private set; }
    
    public PlaceActionPawnActivity(Player player, List<ActionSpace> validActionSpaces) : base(player)
    {
      ValidActionSpaces = validActionSpaces;
    }
    
    public ActionSpace SelectedAction { get; set; }
    
    public override ActivityType Type {
      get { return ActivityType.PlaceActionPawn; }
    }
    
    public override bool IsValid {
      get {
        if (Player == null)
          return false;
        
        return true;
      }
    }
    
    public override void Do (GameController GC)
    {
      GC.PlaceActionPawn(Player, SelectedAction);
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}