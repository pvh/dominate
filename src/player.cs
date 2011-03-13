using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies {
  public enum Species
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

    public Species Species;
    Dictionary<Chit.ElementType, int> adaptation = new Dictionary<Chit.ElementType, int> {
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

    static Dictionary<Species, Chit.ElementType> bonus = new Dictionary<Species, Chit.ElementType>
    {
      { Species.Mammal, Chit.ElementType.Meat },
      { Species.Arachnid, Chit.ElementType.Grub },
      { Species.Bird, Chit.ElementType.Seed },
      { Species.Insect, Chit.ElementType.Grass },
      { Species.Amphibian, Chit.ElementType.Water },
      { Species.Reptile, Chit.ElementType.Sun }
    };

    public Player(Species s)
    {
      Species = s;
      foreach (Chit.ElementType element in Enum.GetValues(typeof(Chit.ElementType)))
      {
        adaptation[element] = 0;
      }
      
      ActionPawns = 6;
      GenePool = 45;
    }

    bool CanAdapt()
    {
      return (adaptation.Values.Sum() + 2 + (Species == Species.Amphibian ? 1 : 0) < MAX_ADAPTATION);
    }

    int Adapt(Chit.ElementType e)
    {
      if (!CanAdapt())
        throw new System.Exception("Already fully adapted.");

      var adapted = adaptation[e] + 1;
      adaptation[e] = adapted;
      return adapted;
    }

    int AdaptationTo(Chit.ElementType e)
    {
      int adapted = adaptation[e];
      if (bonus[Species] == e)
      {
        adapted += 2;
        
        if (Species == Species.Amphibian)
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
