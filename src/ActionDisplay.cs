using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  /* Class is called ActionDisplay because thats what the game calls the ActionSpace area. No display done here */
  public class ActionDisplay
  {
    public enum ActionType
    {
      Invalid,
      Initiative,
      Adaptation,
      Regression,
      Abundance,
      Wasteland,
      Depletion,
      Glaciation,
      Speciation,
      Wanderlust,
      Migration,
      Competition,
      Domination
    }
    
    public SortedDictionary<ActionType, List<ActionSpace>> ActionSpaces = new SortedDictionary<ActionType, List<ActionSpace>> {};
    
    public class ActionSpace {
      public ActionType Type;
      public Player Player { get; set; }
      
      public ActionSpace(ActionType t) {
        Type = t;
      }
    }
    
    public class InitiativeActionSpace : ActionSpace
    {
      public InitiativeActionSpace() : base (ActionType.Initiative) {}
    }
    
    public class AdaptationActionSpace : ActionSpace
    {
      public AdaptationActionSpace() : base (ActionType.Adaptation) {}
    }
    
    public class RegressionActionSpace : ActionSpace
    {
      public RegressionActionSpace() : base (ActionType.Regression) {}
    }
    
    public class AbundanceActionSpace : ActionSpace
    {
      public AbundanceActionSpace() : base (ActionType.Abundance) {}
    }
    
    public class WastelandActionSpace : ActionSpace
    {
      public WastelandActionSpace() : base (ActionType.Wasteland) {}
    }
    
    public class DepletionActionSpace : ActionSpace
    {
      public DepletionActionSpace() : base (ActionType.Depletion) {}
    }
    
    public class GlaciationActionSpace : ActionSpace
    {
      public GlaciationActionSpace() : base (ActionType.Glaciation) {}
    }
    
    public class SpeciationActionSpace : ActionSpace 
    {
      public Chit.ElementType Element;
      public SpeciationActionSpace(Chit.ElementType element) : base (ActionType.Speciation) {
        Element = element;
      }
    }
    
    public class WanderlustActionSpace : ActionSpace
    {
      public WanderlustActionSpace() : base (ActionType.Wanderlust) {}
    }
    
    public class MigrationActionSpace : ActionSpace
    {
      public int Count;
      public MigrationActionSpace(int count) : base (ActionType.Migration) {
        Count = count;
      }
    }
    
    public class CompetitionActionSpace : ActionSpace
    {
      public Tile.TerrainType[] Terrains;
      public CompetitionActionSpace(Tile.TerrainType[] terrains) : base (ActionType.Competition)
      {
        Terrains = terrains;
      }
    }
    
    public class DominationActionSpace : ActionSpace
    {
      public DominationActionSpace() : base (ActionType.Domination) {}
    }
    
    public ActionDisplay() {
      Init();
    }
    
    public void Init() {
      ActionSpaces.Clear();
      foreach (ActionType t in Enum.GetValues(typeof(ActionType))) {
        ActionSpaces[t] = new List<ActionSpace> {};
      }
      
      ActionSpaces[ActionType.Initiative].Add( new InitiativeActionSpace() );
      
      ActionSpaces[ActionType.Adaptation].Add( new AdaptationActionSpace() );
      ActionSpaces[ActionType.Adaptation].Add( new AdaptationActionSpace() );
      ActionSpaces[ActionType.Adaptation].Add( new AdaptationActionSpace() );
      
      ActionSpaces[ActionType.Regression].Add( new RegressionActionSpace() );
      ActionSpaces[ActionType.Regression].Add( new RegressionActionSpace() );
      
      ActionSpaces[ActionType.Abundance].Add( new AbundanceActionSpace() );
      ActionSpaces[ActionType.Abundance].Add( new AbundanceActionSpace() );
      
      ActionSpaces[ActionType.Wasteland].Add( new WastelandActionSpace() );
      
      ActionSpaces[ActionType.Depletion].Add( new DepletionActionSpace() );
      
      ActionSpaces[ActionType.Glaciation].Add( new GlaciationActionSpace() );
      ActionSpaces[ActionType.Glaciation].Add( new GlaciationActionSpace() );
      ActionSpaces[ActionType.Glaciation].Add( new GlaciationActionSpace() );
      ActionSpaces[ActionType.Glaciation].Add( new GlaciationActionSpace() );
      
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Meat) );
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Sun) );
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Seed) );
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Water) );
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Grub) );
      ActionSpaces[ActionType.Speciation].Add( new SpeciationActionSpace(Chit.ElementType.Grass) );
      
      ActionSpaces[ActionType.Wanderlust].Add( new WanderlustActionSpace() );
      ActionSpaces[ActionType.Wanderlust].Add( new WanderlustActionSpace() );
      ActionSpaces[ActionType.Wanderlust].Add( new WanderlustActionSpace() );
      
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(7) );
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(6) );
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(5) );
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(4) );
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(3) );
      ActionSpaces[ActionType.Migration].Add( new MigrationActionSpace(2) );
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Jungle,
        Tile.TerrainType.Wetlands
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Wetlands,
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Desert
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Desert,
        Tile.TerrainType.Forest
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Forest,
        Tile.TerrainType.Savannah
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Savannah,
        Tile.TerrainType.Mountain
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Mountain,
        Tile.TerrainType.Sea
      }));
      
      ActionSpaces[ActionType.Competition].Add( new CompetitionActionSpace( new Tile.TerrainType[] {
        Tile.TerrainType.Tundra,
        Tile.TerrainType.Sea,
        Tile.TerrainType.Jungle
      }));
      
      ActionSpaces[ActionType.Domination].Add( new DominationActionSpace() );
      ActionSpaces[ActionType.Domination].Add( new DominationActionSpace() );
      ActionSpaces[ActionType.Domination].Add( new DominationActionSpace() );
      ActionSpaces[ActionType.Domination].Add( new DominationActionSpace() );
      ActionSpaces[ActionType.Domination].Add( new DominationActionSpace() );
    }
  }
}

