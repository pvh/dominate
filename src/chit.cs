using System;
using System.Collections.Generic;
using System.Linq;

namespace DominantSpecies {
  public class Chit
  {
    public enum ElementType
    {
      None,
      Grass,
      Grub,
      Meat,
      Seed,
      Sun,
      Water,
      Invalid
    }
    
    public ElementType Element { get; set; }
    
    public Chit() : this (ElementType.None)
    {
    }
    
    public Chit(ElementType e)
    {
      Element = e;
    }
  }
  
  public class ChitBag
  {
    List<Chit> chits = new List<Chit> {};
    Random random = new Random();
    
    public ChitBag() {
      for (int i = 0; i < 20; i++) {
        foreach (Chit.ElementType e in Enum.GetValues(typeof(Chit.ElementType))) {
          if (e == Chit.ElementType.Invalid || e == Chit.ElementType.None)
            continue;
          chits.Add(new Chit(e));
        }
      }
    }
    
    public int ChitsLeft() {
      return chits.Count;
    }
    
    public Chit DrawChit() {
      if (ChitsLeft() == 0) return null;
      
      int i = random.Next(chits.Count);
      Chit c = chits[i];
      chits.RemoveAt(i);
      return c;
    }
    
    public Chit DrawChit(Chit.ElementType e)
    {
      if (ChitsLeft() == 0) return null;
      
      Chit chit = chits.Find(c => c.Element == e);
      chits.Remove(chit);
      return chit;
    }
    
    public void ReturnChit(Chit chit) {
      chits.Add(chit);
    }
  }
}
