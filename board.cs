using System;

class DominantSpecies
{
  static int MAP_WIDTH = 10;
  static int MAP_HEIGHT = 10;

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
    Map map;

    Game()
    {
      map = new Map();
      map.tiles[5, 5].tundra = true;

      map.tiles[5, 5].terrain = Tile.Terrain.Sea;
      map.tiles[5, 4].terrain = Tile.Terrain.Forest;
      map.tiles[5, 6].terrain = Tile.Terrain.Savannah;

      map.tiles[4, 5].terrain = Tile.Terrain.Wetlands;
      map.tiles[6, 5].terrain = Tile.Terrain.Mountain;

      map.tiles[6, 6].terrain = Tile.Terrain.Desert;
      map.tiles[4, 4].terrain = Tile.Terrain.Jungle;
    }
  }

  class Player
  {
    DominantSpecies.Species species;

    Player(DominantSpecies.Species s)
    {
      species = s;
    }
  }

  class Map
  {
    internal Tile[,] tiles = new Tile[DominantSpecies.MAP_WIDTH, DominantSpecies.MAP_HEIGHT];
    internal Chit[,] chits = new Chit[DominantSpecies.MAP_WIDTH*2, DominantSpecies.MAP_HEIGHT];

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
  }

  class Chit
  {
    enum Element
    {
      None,
      Grass,
      Grub,
      Meat,
      Seed,
      Sun,
      Water
    }
    
    Element type;
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
    
    Tile(Terrain t, bool isTundra)
    {
      terrain = t; tundra = isTundra;
    }
  }
}
