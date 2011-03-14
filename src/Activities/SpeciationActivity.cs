using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class SpeciationActivity : PlayerActivity
  {
    public List<Chit> SelectableLocations { get; private set; }
    
    public Chit SelectedLocation { get; set; }
    
    public SpeciationActivity(Player player, List<Chit> selectableLocations) : base (player)
    {
      SelectableLocations = selectableLocations;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Speciation; }
    }
    
    public override bool IsValid {
      get {
        return true;
      }
    }
    
    public override void Do (GameController GC)
    {
      List<Tile> tiles = new List<Tile>(GC.TilesFor(SelectedLocation));
      
      tiles.ForEach(delegate(Tile t)
      {
        t.Species[(int) Player.Animal] += t.SpeciateCount;
      });
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}