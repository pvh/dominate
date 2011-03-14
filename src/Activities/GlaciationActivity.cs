using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class GlaciationActivity : PlayerActivity
  {
    public List<Tile> SelectableTiles { get; private set; }
    
    public Tile SelectedTile { get; set; }
    
    public GlaciationActivity(Player player, List<Tile> selectableTiles) : base(player)
    {
      SelectableTiles = selectableTiles;
    }
    
    public override ActivityType Type {
      get { return ActivityType.GlaciationSpace; }
    }
    
    public override bool IsValid {
      get {
        return true;
      }
    }
    
    public override void Do (GameController GC)
    {
      SelectedTile.Tundra = true;
      foreach (Animal animal in Enum.GetValues(typeof(Animal)))
      {
        if (SelectedTile.Species[(int)animal] > 1) {
          GC.AddSpeciesToGenePool(animal, SelectedTile.Species[(int)animal] - 1);
          SelectedTile.Species[(int)animal] = 1;
        }
      }
    }
    
    public override void Undo (GameController GC)
    {
      throw new NotImplementedException ();
    }
  }
}