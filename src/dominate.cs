using System;
using DominantSpecies;

class Application
{
  static string TileString(Tile tile)
  {
    if (tile == null)
      return "<!>";

    switch(tile.Terrain)
      {
      case Tile.TerrainType.Invalid:
        return "   ";
      case Tile.TerrainType.Empty:
        return "<.>";
      case Tile.TerrainType.Sea:
        return "<S>";
      case Tile.TerrainType.Forest:
        return "<F>";
      case Tile.TerrainType.Savannah:
        return "<V>";
      case Tile.TerrainType.Wetlands:
        return "<W>";
      case Tile.TerrainType.Mountain:
        return "<M>";
      case Tile.TerrainType.Desert:
        return "<D>";
      case Tile.TerrainType.Jungle:
        return "<J>";
      }
    return "<?>";
  }
  static string ChitString(Chit chit)
  {
    if (chit == null)
      return " ";

    switch(chit.element)
      {
      case Chit.Element.None:
        return ".";
      case Chit.Element.Sun:
        return "s";
      case Chit.Element.Grub:
        return "g";
      case Chit.Element.Water:
        return "w";
      case Chit.Element.Meat:
        return "m";
      case Chit.Element.Seed:
        return "d";
      case Chit.Element.Grass:
        return "r";
      }
    return "?";
  }
  static void PrintGame(Game game)
  {
    Console.WriteLine("--------");
    var map = game.map;
    for (int j=0; j <= map.tiles.GetUpperBound(1); j++)
      Console.Write("{0}   ", j);
    Console.WriteLine();

    for (int i=0; i <= map.chits.GetUpperBound(0); i++)
      {
        // write the chits
        for (int spaces=0; spaces < i; spaces++)
          {
            Console.Write("  ");
          }
        for (int j=0; j <= map.chits.GetUpperBound(1); j++)
          {
            Console.Write("{0} ", ChitString(map.chits[i,j]));
          }
        Console.WriteLine();

        // write the tiles (no tiles for the last row of chits)
        if (i > map.tiles.GetUpperBound(0)) { break; }

        for (int spaces=0; spaces < i; spaces++)
          {
            Console.Write("  ");
          }
        Console.Write(" ");

        for (int j=0; j <= map.tiles.GetUpperBound(1); j++)
          {
            Console.Write("{0} ", TileString(map.tiles[i,j]));
          }
        Console.WriteLine(" <--- i:{0}", i);
      }
    Console.WriteLine("--------");

    foreach (var chit in map.ChitsFor(3,3))
      {
        Console.WriteLine(chit.element);
      }
  }
  static void Main()
  {
    var game = new DominantSpecies.Game();
    PrintGame(game);
  }
}
