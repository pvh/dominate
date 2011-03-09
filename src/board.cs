using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies
{
  public enum Species
  {
    Mammal,
    Reptile,
    Bird,
    Amphibian,
    Arachnid,
    Insect
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

}
