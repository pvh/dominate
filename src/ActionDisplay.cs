using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  /* Class is called ActionDisplay because thats what the game calls the ActionSpace area. No display done here */
  public class ActionDisplay
  {
    public SortedDictionary<ActionType, List<ActionSpace>> ActionSpaces = new SortedDictionary<ActionType, List<ActionSpace>> {};

    public List<Chit> AdaptationChits = new List<Chit> {};
    public List<Chit> RegressionChits = new List<Chit> {};
    public List<Chit> AbundanceChits = new List<Chit> {};
    public List<Chit> WastelandChits = new List<Chit> {};
    public List<Chit> DepletionChits = new List<Chit> {};
    public List<Chit> WanderlustChits = new List<Chit> {};
    
    // We may just want the ActionDisplay to own the chit bag.
    public ChitBag ChitBag = new ChitBag();
    
    public ActionDisplay() {
      Init();
    }
    
    public List<ActionSpace> AvailableActionSpaces
    {
      get
      {
        List<ActionSpace> availableActionSpaces = new List<ActionSpace>();
        
        foreach (var kvp in ActionSpaces)
        {
          availableActionSpaces.AddRange(kvp.Value.FindAll(space => space.Player == null));
        }
        
        return availableActionSpaces;
      }
    }
    
    public void Init() {
      CreateActionSpaces();
      RefreshChits();
    }
    
    public void RefreshChits() {
      // This should probably be an activity instead
      // to make it easier to animate
      foreach (var chit in DepletionChits) {
        ChitBag.ReturnChit(chit);
      }
      
      DepletionChits = WastelandChits;
      WastelandChits = AbundanceChits;
      AbundanceChits = new List<Chit> {};
      
      foreach (var chit in RegressionChits) {
        ChitBag.ReturnChit(chit);
      }
      
      foreach (var chit in WanderlustChits) {
        ChitBag.ReturnChit(chit);
      }
      
      RegressionChits = AdaptationChits;
      AdaptationChits = new List<Chit> {};
      
      for (int i = 0; i < 4; i++) {
        AdaptationChits.Add(ChitBag.DrawChit());
        AbundanceChits.Add(ChitBag.DrawChit());
        WanderlustChits.Add(ChitBag.DrawChit());
      }      
    }
    
    public void CreateActionSpaces() {
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

