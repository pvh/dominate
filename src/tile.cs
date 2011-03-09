namespace DominantSpecies {
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
