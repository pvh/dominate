using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  public enum ActivityType
  {
    Abundance
  }
  
  public abstract class Activity
  {
    public Activity ()
    {
    }
    
    public abstract bool IsValid { get; }
    
    public abstract void Do();
    
    public abstract void Undo();
    
    public abstract ActivityType Type { get; }
  }
    
  public class AbundanceActivity : Activity
  {
    public List<Chit.ElementType> ValidTypes { get; private set; }
    public List<Chit> ValidChits { get; private set; }
    
    public Chit SelectedChit { get; set; }
    public Chit.ElementType SelectedElementType { get; set; }
    
    public AbundanceActivity(Chit.ElementType[] validTypes, Chit[] validChits) : this(new List<Chit.ElementType>(validTypes),
                                                                                      new List<Chit>(validChits))
    {
      SelectedElementType = Chit.ElementType.None;
    }
    
    public AbundanceActivity(List<Chit.ElementType> validTypes, List<Chit> validChits)
    {
      ValidTypes = validTypes;
      ValidChits = validChits;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Abundance; }
    }
    
    public override bool IsValid
    {
      get
      {
        if (SelectedChit == null || SelectedElementType == Chit.ElementType.None)
        {
          return false;
        }
        
        return true;
      }
    }
    
    public override void Do()
    {
    }
    
    public override void Undo()
    {
    }
  }
}