using System;
using System.Collections.Generic;

namespace DominantSpecies.Activities
{
  public class AbundanceActivity : PlayerActivity
  {
    // Question: can these both not be public?
    public Map Map { get; private set; }
    public List<Chit> AvailableChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public AbundanceActivity(Player player, List<Chit> availableChits, Map map) : base (player)
    {
      Map = map;
      AvailableChits = availableChits;
      
      // This is wrong, as it should be chits only on placed tiles, but it works.
      // Chit[] validChitLocations = g.map.Chits.All.FindAll(chit => chit.Element == Chit.ElementType.None).ToArray();
        
      SelectedElementType = Chit.ElementType.None;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Abundance; }
    }
    
    public override bool IsValid
    {
      get
      {
        if (SelectedChit == null || SelectedElementType == Chit.ElementType.None)
        {
          return false;
        }
        
        return true;
      }
    }
    
    public override void Do(GameController GC)
    {
      GC.PlaceChit(SelectedChit, SelectedElementType);
    }
    
    public override void Undo(GameController GC)
    {
      GC.RemoveChit(SelectedChit);
    }
  }
}