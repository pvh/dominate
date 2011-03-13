using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies {

  public class Game
  {
    public Map map { get; set; }
    public List<Player> Players = new List<Player> {};
  
    public Player PlayerFor(Animal a) {
      return Players.Find(p => p.Animal == a);
    }
    
    public Player DominatedBy(Tile t) {
      var scoredPlayers = Players.OrderByDescending(p => {
        // highest domination score for a player with >0 species
        return (t.Species[(int)p.Animal] == 0) ? 0 : p.DominationScoreOn(map, t);
      });

      // Doesn't count if you have a score of 0
      if (scoredPlayers.First().DominationScoreOn(map, t) == 0) {
        return null;
      }
      
      // Ties go to nobody.
      if (scoredPlayers.First().DominationScoreOn(map, t) == 
          scoredPlayers.ElementAt(1).DominationScoreOn(map, t))
        return null;

      // Otherwise, highest wins
      return scoredPlayers.First();
    }
    
    public Dictionary<Player, int> ScoreFor(Tile t)
    {
      var delta = new Dictionary<Player, int> {};
      
      var ranks = t.ScoreValues;
      var currentRank = 0;
      
      foreach (var player in Players.OrderByDescending(player => t.Species[(int) player.Animal])) {
        // Stop if we've given out all the points or run out of players
        if (currentRank >= ranks.Length) break;
        if (t.Species[(int) player.Animal] == 0) break;
        
        delta.Add(player, ranks[currentRank++]);
      }
      
      return delta;
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
      map.tiles[i,   j-1].Species[(int) Animal.Bird] = 2;
      map.tiles[i,   j-1].Species[(int) Animal.Arachnid] = 1;
      map.tiles[i,   j-1].Species[(int) Animal.Mammal] = 1;

      map.tiles[i,   j+1].Terrain = Tile.TerrainType.Savannah;
      map.tiles[i,   j+1].Species[(int) Animal.Insect] = 2;
      map.tiles[i,   j+1].Species[(int) Animal.Reptile] = 1;
      map.tiles[i,   j+1].Species[(int) Animal.Amphibian] = 1;

      map.tiles[i-1, j  ].Terrain = Tile.TerrainType.Jungle;
      map.tiles[i-1, j  ].Species[(int) Animal.Arachnid] = 2;
      map.tiles[i-1, j  ].Species[(int) Animal.Amphibian] = 1;
      map.tiles[i-1, j  ].Species[(int) Animal.Bird] = 1;

      map.tiles[i-1, j+1].Terrain = Tile.TerrainType.Wetlands;
      map.tiles[i-1, j+1].Species[(int) Animal.Amphibian] = 2;
      map.tiles[i-1, j+1].Species[(int) Animal.Insect] = 1;
      map.tiles[i-1, j+1].Species[(int) Animal.Arachnid] = 1;

      map.tiles[i+1, j-1].Terrain = Tile.TerrainType.Mountain;
      map.tiles[i+1, j-1].Species[(int) Animal.Mammal] = 2;
      map.tiles[i+1, j-1].Species[(int) Animal.Reptile] = 1;
      map.tiles[i+1, j-1].Species[(int) Animal.Bird] = 1;

      map.tiles[i+1, j  ].Terrain = Tile.TerrainType.Desert;
      map.tiles[i+1, j  ].Species[(int) Animal.Reptile] = 2;
      map.tiles[i+1, j  ].Species[(int) Animal.Insect] = 1;
      map.tiles[i+1, j  ].Species[(int) Animal.Mammal] = 1;

      // Chit are double-wide along j.
      j = j*2;
      map.chits[i,   j  ] = new Chit(Chit.ElementType.Grass);
      map.chits[i,   j+1] = new Chit(Chit.ElementType.Grub);

      map.chits[i+1, j  ] = new Chit(Chit.ElementType.Meat);
      map.chits[i+1, j+1] = new Chit(Chit.ElementType.Seed);

      map.chits[i,   j+2] = new Chit(Chit.ElementType.Sun);
      map.chits[i+1, j-1] = new Chit(Chit.ElementType.Water);
    }

    public Game(bool defaultSetup = true)
    {
      Players.Add(new Player(Animal.Amphibian));
      Players.Add(new Player(Animal.Insect));
      Players.Add(new Player(Animal.Arachnid));
      Players.Add(new Player(Animal.Mammal));
      Players.Add(new Player(Animal.Bird));
      Players.Add(new Player(Animal.Reptile));
      
      map = new Map();
      BlankOutMapTiles();
      if (defaultSetup)
        DefaultSetup();
    }
  }

}
