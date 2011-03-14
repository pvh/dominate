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
    
    public ActionDisplay() {
      Init();
      
      // FIXME: Garbage data
      AdaptationChits.Add(new Chit(Chit.ElementType.Grub));
      AbundanceChits.Add(new Chit(Chit.ElementType.Grub));
      DepletionChits.Add(new Chit(Chit.ElementType.Grub));
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

