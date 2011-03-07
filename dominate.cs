using System;
using DominantSpecies;

class Application
{
  static string TileString(Tile tile)
  {
    if (tile == null)
      return "< >";

    switch(tile.terrain)
      {
      case Tile.Terrain.Empty:
        return "<.>";
      case Tile.Terrain.Sea:
        return "<S>";
      case Tile.Terrain.Forest:
        return "<F>";
      case Tile.Terrain.Savannah:
        return "<V>";
      case Tile.Terrain.Wetlands:
        return "<W>";
      case Tile.Terrain.Mountain:
        return "<M>";
      case Tile.Terrain.Desert:
        return "<D>";
      case Tile.Terrain.Jungle:
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
    for (int i=0; i <= map.tiles.GetUpperBound(0); i++)
      {
        for (int spaces=0; spaces < i; spaces++)
          {
            Console.Write("   ");
          }
        for (int j=0; j <= map.chits.GetUpperBound(1); j++)
          {
            Console.Write("{0} ", ChitString(map.chits[i,j]));
            if (j % 2 == 1)
              Console.Write(" ");
          }
        Console.WriteLine();

        for (int spaces=0; spaces < i; spaces++)
          {
            Console.Write("   ");
          }
        for (int j=0; j <= map.tiles.GetUpperBound(1); j++)
          {
            Console.Write("{0}  ", TileString(map.tiles[i,j]));
          }
        Console.WriteLine();
      }
    Console.WriteLine("--------");
  }
  static void Main()
  {
    var game = new DominantSpecies.Game();
    PrintGame(game);
  }
}
