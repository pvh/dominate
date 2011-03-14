using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  public enum ActivityType
  {
    /* Activity types for actions on the ActionDisplay */
    Adaptation,
    Abundance,
    Speciation,
    
    /* Planning phase activities */
    PlaceActionPawn
  }
  
  public abstract class Activity
  {
    public Activity ()
    {
    }
    
    public abstract bool IsValid { get; }
    
    public abstract void Do(GameController GC);
    
    public abstract void Undo(GameController GC);
    
    public abstract ActivityType Type { get; }
  }
  
  public abstract class PlayerActivity : Activity
  {
    public Player Player { get; private set; }
    
    public PlayerActivity(Player player)
    {
      Player = player;
    }
  }
  
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
    }
  }
  
  public class AbundanceActivity : PlayerActivity
  {
    public List<Chit.ElementType> ValidTypes { get; private set; }
    public List<Chit> ValidChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public AbundanceActivity(Player player, Chit.ElementType[] validTypes, Chit[] validChits) : this(player,
                                                                                                     new List<Chit.ElementType>(validTypes),
                                                                                                     new List<Chit>(validChits))
    {
    }
    
    public AbundanceActivity(Player player, List<Chit.ElementType> validTypes, List<Chit> validChits) : base (player)
    {
      ValidTypes = validTypes;
      ValidChits = validChits;
      
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
  
  public class PlaceActionPawnActivity : PlayerActivity
  {
    public PlaceActionPawnActivity(Player player) : base(player)
    {
    }
    
    public ActivityType SelectedAction { get; set; }
    
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