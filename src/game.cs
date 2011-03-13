using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies {

  public class Game
  {
    public Map map { get; set; }
    public List<Player> Players = new List<Player> {};

    public Player DominatedBy(Tile t) {
      return Players.OrderByDescending(p => p.DominationScoreOn(map, t)).First();
    }
    
    void BlankOutMapTiles() {
      // Cut out the corners of the map.
      map.tiles[0, 0].Terrain = Tile.TerrainType.Invalid;
      map.tiles[0, 1].Terrain = Tile.TerrainType.Invalid;
      map.tiles[0, 2].Terrain = Tile.TerrainType.Invalid;
      map.tiles[1, 0].Terrain = Tile.TerrainType.Invalid;
      map.tiles[1, 1].Terrain = Tile.TerrainType.Invalid;
      map.tiles[2, 0].Terrain = Tile.TerrainType.Invalid;

      map.tiles[3, 0].Terrain = Tile.TerrainType.Invalid;
      map.tiles[3, 6].Terrain = Tile.TerrainType.Invalid;

      map.tiles[4, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[5, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[5, 5].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 6].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 5].Terrain = Tile.TerrainType.Invalid;
      map.tiles[6, 4].Terrain = Tile.TerrainType.Invalid;
    }

    void DefaultSetup() {
      int i = 3;
      int j = 3;

      map.tiles[i,   j  ].Terrain = Tile.TerrainType.Sea;
      map.tiles[i,   j  ].Tundra = true;

      map.tiles[i,   j-1].Terrain = Tile.TerrainType.Forest;
      map.tiles[i,   j-1].Species[(int) Species.Bird] = 2;
      map.tiles[i,   j-1].Species[(int) Species.Arachnid] = 1;
      map.tiles[i,   j-1].Species[(int) Species.Mammal] = 1;

      map.tiles[i,   j+1].Terrain = Tile.TerrainType.Savannah;
      map.tiles[i,   j+1].Species[(int) Species.Insect] = 2;
      map.tiles[i,   j+1].Species[(int) Species.Reptile] = 1;
      map.tiles[i,   j+1].Species[(int) Species.Amphibian] = 1;

      map.tiles[i-1, j  ].Terrain = Tile.TerrainType.Jungle;
      map.tiles[i-1, j  ].Species[(int) Species.Arachnid] = 2;
      map.tiles[i-1, j  ].Species[(int) Species.Amphibian] = 1;
      map.tiles[i-1, j  ].Species[(int) Species.Bird] = 1;

      map.tiles[i-1, j+1].Terrain = Tile.TerrainType.Wetlands;
      map.tiles[i-1, j+1].Species[(int) Species.Amphibian] = 2;
      map.tiles[i-1, j+1].Species[(int) Species.Insect] = 1;
      map.tiles[i-1, j+1].Species[(int) Species.Arachnid] = 1;

      map.tiles[i+1, j-1].Terrain = Tile.TerrainType.Mountain;
      map.tiles[i+1, j-1].Species[(int) Species.Mammal] = 2;
      map.tiles[i+1, j-1].Species[(int) Species.Reptile] = 1;
      map.tiles[i+1, j-1].Species[(int) Species.Bird] = 1;

      map.tiles[i+1, j  ].Terrain = Tile.TerrainType.Desert;
      map.tiles[i+1, j  ].Species[(int) Species.Reptile] = 2;
      map.tiles[i+1, j  ].Species[(int) Species.Insect] = 1;
      map.tiles[i+1, j  ].Species[(int) Species.Mammal] = 1;

      // Chit are double-wide along j.
      j = j*2;
      map.chits[i,   j  ] = new Chit(Chit.ElementType.Grass);
      map.chits[i,   j+1] = new Chit(Chit.ElementType.Grub);

      map.chits[i+1, j  ] = new Chit(Chit.ElementType.Meat);
      map.chits[i+1, j+1] = new Chit(Chit.ElementType.Seed);

      map.chits[i,   j+2] = new Chit(Chit.ElementType.Sun);
      map.chits[i+1, j-1] = new Chit(Chit.ElementType.Water);
    }

    public Game()
    {
      Players.Add(new Player(Species.Amphibian));
      Players.Add(new Player(Species.Insect));
      Players.Add(new Player(Species.Arachnid));
      Players.Add(new Player(Species.Mammal));
      Players.Add(new Player(Species.Bird));
      Players.Add(new Player(Species.Reptile));
      
      map = new Map();
      BlankOutMapTiles();
      DefaultSetup();
    }
  }

}
