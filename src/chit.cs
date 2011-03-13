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
    
    public ElementType Element { get; internal set; }
    
    public Chit() : this (ElementType.None)
    {
    }
    
    public Chit(ElementType e)
    {
      Element = e;
    }
  }
}
