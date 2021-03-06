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

    public int[] ScoreValues
    {
      get
        {
          switch(this.Terrain) {
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

    public int SpeciateCount
    {
      get {
          if (Tundra) {
            return 1;
          }
          
          switch(this.Terrain) {
            case TerrainType.Sea:
            case TerrainType.Wetlands:
              return 4;
            case TerrainType.Savannah:
            case TerrainType.Jungle:
            case TerrainType.Forest:
              return 3;
            case TerrainType.Desert:
            case TerrainType.Mountain:
              return 2;
          }
          return 0;
      }
    }

    public bool Tundra
    {
      get; set;
    }
    public int[] Species = new int[6];
    public TerrainType Terrain { get; set; }
  }
}
