using System;

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

  class Game
  {
    internal Map map;

    internal Game()
    {
      map = new Map();

      int r = 3;
      int c = 3;

      map.tiles[r,   c  ] = new Tile(Tile.Terrain.Sea, true);
      map.tiles[r,   c-1] = new Tile(Tile.Terrain.Forest);
      map.tiles[r,   c+1] = new Tile(Tile.Terrain.Savannah);
      map.tiles[r-1, c  ] = new Tile(Tile.Terrain.Jungle);
      map.tiles[r-1, c+1] = new Tile(Tile.Terrain.Wetlands);
      map.tiles[r+1, c-1] = new Tile(Tile.Terrain.Mountain);
      map.tiles[r+1, c  ] = new Tile(Tile.Terrain.Desert);

      // Chit rows are double-wide.
      c = c*2;
      map.chits[r,   c  ] = new Chit(Chit.Element.Grass);
      map.chits[r,   c+1] = new Chit(Chit.Element.Grub);

      map.chits[r+1, c  ] = new Chit(Chit.Element.Meat);
      map.chits[r+1, c+1] = new Chit(Chit.Element.Seed);

      map.chits[r,   c+2] = new Chit(Chit.Element.Sun);
      map.chits[r+1, c-1] = new Chit(Chit.Element.Water);

    }
  }

  class Player
  {
    Species species;

    Player(Species s)
    {
      species = s;
    }
  }

  class Map
  {
    static int MAP_WIDTH = 7;
    static int MAP_HEIGHT = 7;

    internal Tile[,] tiles = new Tile[MAP_HEIGHT, MAP_WIDTH];
    internal Chit[,] chits = new Chit[MAP_HEIGHT, MAP_WIDTH*2];

    Tile TileAt(int i, int j)
    {
      return tiles[i, j];
    }

    Chit[] ChitsFor(int i, int j)
    {
      // We map chits to a double-width array
      return new Chit[] { chits[i,   2*j],     chits[i, (2*j)+1],
                          chits[i-1, 2*j],     chits[i-1, (2*j)+1],
                          chits[i+1, (2*j)+2], chits[i-1, (2*j)-1] };
    }

    public Map()
    {
      for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        for (int j = 0; j <= tiles.GetUpperBound(1); j++)
          tiles[i,j] = new Tile();

      for (int i = 0; i <= chits.GetUpperBound(0); i++)
        for (int j = 0; j <= chits.GetUpperBound(1); j++)
          chits[i,j] = new Chit();

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

  class Tile
  {
    internal enum Terrain
    {
      Empty,
      Sea,
      Wetlands,
      Savannah,
      Jungle,
      Forest,
      Desert,
      Mountain,
      Tundra
    }

    int[] scoreValues
    {
      get
        {
          switch(this.terrain)
            {
            case Terrain.Sea:
              return new int[] { 9, 5, 3, 2 };
            case Terrain.Wetlands:
              return new int[] { 8, 4, 2, 1 };
            case Terrain.Savannah:
              return new int[] { 7, 4, 2 };
            case Terrain.Jungle:
              return new int[] { 6, 3, 2 };
            case Terrain.Forest:
              return new int[] { 5, 3, 2 };
            case Terrain.Desert:
              return new int[] { 4, 2 };
            case Terrain.Mountain:
              return new int[] { 3, 2 };
            case Terrain.Tundra:
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
    internal Terrain terrain;

    internal Tile()
    {
      terrain = Terrain.Empty;
      tundra = false;
    }
 
    internal Tile(Terrain t)
    {
      terrain = t; tundra = false;
    }

    internal Tile(Terrain t, bool isTundra)
    {
      terrain = t; tundra = isTundra;
    }
  }
}
