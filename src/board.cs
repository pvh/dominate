using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies
{

  enum Species
  {
    Mammal,
    Reptile,
    Bird,
    Amphibian,
    Arachnid,
    Insect
  }

  public class Game
  {
    public Map map { get; set; }

    void BlankOutMapTiles() {
      // Cut out the corners of the map.
      map.tiles[0, 0].Terrain = Tile.TerrainType.Invalid;
      map.tiles[0, 1].Terrain = Tile.TerrainType.Invalid;
      map.tiles[0, 2].Terrain = Tile.TerrainType.Invalid;
      map.tiles[0, 3].Terrain = Tile.TerrainType.Invalid;
      map.tiles[1, 0].Terrain = Tile.TerrainType.Invalid;
      map.tiles[1, 1].Terrain = Tile.TerrainType.Invalid;
      map.tiles[2, 0].Terrain = Tile.TerrainType.Invalid;

      map.tiles[4, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[5, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[5, 5].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 5].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 4].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 3].Terrain = Tile.TerrainType.Invalid;
    }

    void DefaultSetup() {
      int i = 3;
      int j = 3;

      map.tiles[i,   j  ].Terrain = Tile.TerrainType.Sea;
      map.tiles[i,   j  ].tundra = true;
      map.tiles[i,   j-1].Terrain = Tile.TerrainType.Forest;
      map.tiles[i,   j+1].Terrain = Tile.TerrainType.Savannah;
      map.tiles[i-1, j  ].Terrain = Tile.TerrainType.Jungle;
      map.tiles[i-1, j+1].Terrain = Tile.TerrainType.Wetlands;
      map.tiles[i+1, j-1].Terrain = Tile.TerrainType.Mountain;
      map.tiles[i+1, j  ].Terrain = Tile.TerrainType.Desert;

      // Chit are double-wide along j.
      j = j*2;
      map.chits[i,   j  ] = new Chit(Chit.Element.Grass);
      map.chits[i,   j+1] = new Chit(Chit.Element.Grub);

      map.chits[i+1, j  ] = new Chit(Chit.Element.Meat);
      map.chits[i+1, j+1] = new Chit(Chit.Element.Seed);

      map.chits[i,   j+2] = new Chit(Chit.Element.Sun);
      map.chits[i+1, j-1] = new Chit(Chit.Element.Water);
    }

    public Game()
    {
      map = new Map();
      BlankOutMapTiles();
      DefaultSetup();
    }
  }

  class Player
  {
    static int MAX_ADAPTATION = 6;

    Species species;
    Dictionary<Chit.Element, int> adaptation;

    static Dictionary<Species, Chit.Element> bonus = new Dictionary<Species, Chit.Element>
      {
        { Species.Mammal, Chit.Element.Meat },
        { Species.Arachnid, Chit.Element.Grub }
      };

    Player(Species s)
    {
      species = s;
      foreach (Chit.Element element in Enum.GetValues(typeof(Chit.Element)))
        {
          adaptation[element] = 0;
        }
    }

    bool CanAdapt()
    {
      return (adaptation.Values.Sum() < MAX_ADAPTATION);
    }

    int Adapt(Chit.Element e)
    {
      if (!CanAdapt())
        throw new System.Exception("Already fully adapted.");

      var adapted = adaptation[e] + 1;
      adaptation[e] = adapted;
      return adapted;
    }

    int AdaptationTo(Chit.Element e)
    {
      int adapted = adaptation[e];
      if (bonus[species] == e)
        adapted += 2;
      return adapted;
    }

    int DominationScoreOn(Map m, int i, int j)
    {
      var sum = 0;
      foreach (var chit in m.ChitsFor(i, j))
        {
          sum += AdaptationTo(chit.element);
        }
      return sum;
    }
  }

  public class Map
  {
    static int MAP_WIDTH = 7;
    static int MAP_HEIGHT = 7;

    internal Tile[,] tiles = new Tile[MAP_HEIGHT, MAP_WIDTH];

    // We assign the chits positions as though they were placed
    // at the top
    internal Chit[,] chits = new Chit[MAP_HEIGHT + 1, MAP_WIDTH*2];

    public Tiles Tiles { get; set; }

    internal Chit[] ChitsFor(int i, int j)
    {
      // We map chits to a double-width array
      j *= 2;
      return new Chit[] { chits[i,   j], chits[i, j+1],
                          chits[i+1, j-1], chits[i, j+2],
                          chits[i+1, j], chits[i+1, j+1] };
    }

    internal void PlaceChit(int i, int j, Chit.Element e)
    {
      chits[i, j].element = e;
    }
    public void RemoveChit(int i, int j)
    {
      chits[i, j].element = Chit.Element.None;
    }

    internal void PlaceTile(int i, int j, Tile.TerrainType t)
    {
      tiles[i, j].Terrain = t;
    }

    public void Glaciate(int i, int j)
    {
      // TODO: reduce species to 1 of each present
      tiles[i, j].tundra = true;
    }

    public Map()
    {
      for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        for (int j = 0; j <= tiles.GetUpperBound(1); j++)
          tiles[i,j] = new Tile(i, j);
          
      Tiles = new Tiles(tiles);

      for (int i = 0; i <= chits.GetUpperBound(0); i++)
        for (int j = 0; j <= chits.GetUpperBound(1); j++)
          chits[i,j] = new Chit();

    }
  }
  
  public class Tiles
  {
    private Tile[,] tiles;
    
    public Tiles(Tile[,] t)
    {
      tiles = t;
    }
    
    public Tile this [int i, int j]
    {
      get { return tiles[i, j]; }
    }
    
    private List<Tile> _flatList;
    public List<Tile> All
    {
      get {
        if (_flatList == null)
        {
          _flatList = new List<Tile>();
      
          for (int i = 0; i <= tiles.GetUpperBound(0); i++)
            for (int j = 0; j <= tiles.GetUpperBound(1); j++)
              _flatList.Add(tiles[i, j]);
        }
        return _flatList;
      }
    }
  }
  
  class Chit
  {
    public enum Element
    {
      None,
      Grass,
      Grub,
      Meat,
      Seed,
      Sun,
      Water
    }
    
    public Element element;

    public Chit()
    {
      element = Element.None;
    }

    public Chit(Element e)
    {
      element = e;
    }
  }

  public class Tile
  {
    public enum TerrainType
    {
      Empty,
      Sea,
      Wetlands,
      Savannah,
      Jungle,
      Forest,
      Desert,
      Mountain,
      Tundra,
      Invalid
    }

    int[] scoreValues
    {
      get
        {
          switch(this.Terrain)
            {
            case TerrainType.Sea:
              return new int[] { 9, 5, 3, 2 };
            case TerrainType.Wetlands:
              return new int[] { 8, 4, 2, 1 };
            case TerrainType.Savannah:
              return new int[] { 7, 4, 2 };
            case TerrainType.Jungle:
              return new int[] { 6, 3, 2 };
            case TerrainType.Forest:
              return new int[] { 5, 3, 2 };
            case TerrainType.Desert:
              return new int[] { 4, 2 };
            case TerrainType.Mountain:
              return new int[] { 3, 2 };
            case TerrainType.Tundra:
              return new int[] { 1 };
            }
          return new int[] { };
        }
    }

    internal bool tundra
    {
      get; set;
    }
    internal int[] Species = new int[6];
    public TerrainType Terrain { get; set; }
		
	public int I { get; private set; }
	public int J { get; private set; }

    internal Tile(int i, int j)
    {
      I = i;
      J = j;
    }
  }
}
