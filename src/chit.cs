namespace DominantSpecies {
  public class Chit
  {
    public enum Element
    {
      None,
      Grass,
      Grub,
      Meat,
      Seed,
      Sun,
      Water
    }

    public Element element;

    public Chit()
    {
      element = Element.None;
    }

    public Chit(Element e)
    {
      element = e;
    }
  }

}
