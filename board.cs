using System;


class DominantSpecies
{
  static int BOARD_WIDTH = 10;
  static int BOARD_HEIGHT = 10;

  enum Species
  {
    Mammal,
    Reptile,
    Bird,
    Amphibian,
    Arachnid,
    Insect
  }

  class Player
  {
    DominantSpecies.Species species;

    Player(DominantSpecies.Species s)
    {
      species = s;
    }
  }

  class Board
  {
    Tile[,] tiles = new Tile[DominantSpecies.BOARD_WIDTH, DominantSpecies.BOARD_HEIGHT];
    Chit[,] chits = new Chit[DominantSpecies.BOARD_WIDTH*2, DominantSpecies.BOARD_HEIGHT];

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
    enum Terrain
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

    bool tundra;
    int[] species = new int[6];
    Terrain terrain;
    
    Tile(Terrain t, bool isTundra)
    {
      terrain = t; tundra = isTundra;
    }
  }
}
