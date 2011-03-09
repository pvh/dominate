namespace DominantSpecies {

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

}
