using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies {
  public enum Animal
  {
    Mammal,
    Reptile,
    Bird,
    Amphibian,
    Arachnid,
    Insect
  }

  public class Player
  {
    static int MAX_ADAPTATION = 6;

    public Animal Animal;
    private Dictionary<Chit.ElementType, int> adaptation = new Dictionary<Chit.ElementType, int>() {
      {Chit.ElementType.Water, 0},
      {Chit.ElementType.Sun, 0},
      {Chit.ElementType.Meat, 0},
      {Chit.ElementType.Grass, 0},
      {Chit.ElementType.Seed, 0},
      {Chit.ElementType.Grub, 0}
    };
    public int ActionPawns { get; set; }
    public int GenePool { get; set; }
    public int Score { get; set; }

    static Dictionary<Animal, Chit.ElementType> bonus = new Dictionary<Animal, Chit.ElementType>()
    {
      { Animal.Mammal, Chit.ElementType.Meat },
      { Animal.Arachnid, Chit.ElementType.Grub },
      { Animal.Bird, Chit.ElementType.Seed },
      { Animal.Insect, Chit.ElementType.Grass },
      { Animal.Amphibian, Chit.ElementType.Water },
      { Animal.Reptile, Chit.ElementType.Sun }
    };

    public Player(Animal s)
      : this(s, 6)
    {
    }
    
    public Player(Animal s, int numberOfPlayers)
    {
      Animal = s;
      ActionPawns = 7 - (numberOfPlayers - 2);
      GenePool = 55 - (numberOfPlayers - 2) * 5;
    }

    bool CanAdapt()
    {
      return (adaptation.Values.Sum() + 2 + (Animal == Animal.Amphibian ? 1 : 0) < MAX_ADAPTATION);
    }

    internal int Adapt(Chit.ElementType e)
    {
      if (!CanAdapt())
        throw new System.Exception("Already fully adapted.");

      var adapted = adaptation[e] + 1;
      adaptation[e] = adapted;
      return adapted;
    }

    public int AdaptationTo(Chit.ElementType e)
    {
      if (e == Chit.ElementType.None || e == Chit.ElementType.Invalid)
      {
        return 0;
      }
      
      int adapted = adaptation[e];
      if (bonus[Animal] == e)
      {
        adapted += 2;
        
        if (Animal == Animal.Amphibian)
        {
          adapted++;
        }
      }
      return adapted;
    }

    public int DominationScoreOn(Map m, Tile t)
    {
      return m.ChitsFor(t).Sum(chit => AdaptationTo(chit.Element));
    }
  }

}
