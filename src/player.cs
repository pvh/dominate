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

    public Species species;
    Dictionary<Chit.Element, int> adaptation;

    static Dictionary<Species, Chit.Element> bonus = new Dictionary<Species, Chit.Element>
      {
        { Species.Mammal, Chit.Element.Meat },
        { Species.Arachnid, Chit.Element.Grub }
      };

    Player(Species s)
    {
      species = s;
      foreach (Chit.Element element in Enum.GetValues(typeof(Chit.Element)))
        {
          adaptation[element] = 0;
        }
    }

    bool CanAdapt()
    {
      return (adaptation.Values.Sum() < MAX_ADAPTATION);
    }

    int Adapt(Chit.Element e)
    {
      if (!CanAdapt())
        throw new System.Exception("Already fully adapted.");

      var adapted = adaptation[e] + 1;
      adaptation[e] = adapted;
      return adapted;
    }

    int AdaptationTo(Chit.Element e)
    {
      int adapted = adaptation[e];
      if (bonus[species] == e)
        adapted += 2;
      return adapted;
    }

    int DominationScoreOn(Map m, int i, int j)
    {
      var sum = 0;
      foreach (var chit in m.ChitsFor(i, j))
        {
          sum += AdaptationTo(chit.element);
        }
      return sum;
    }
  }

}
