using System;
using System.Collections.Generic;

namespace DominantSpecies
{
  public enum ActivityType
  {
    Speciation,
    Abundance
  }
  
  public abstract class Activity
  {
    public Activity ()
    {
    }
    
    public abstract void Do();
    
    public abstract void Undo();
    
    public abstract ActivityType Type { get; }
  }
  
  public class SpeciationActivity : Activity
  {
    public override ActivityType Type {
        get { return ActivityType.Speciation; }
    }
    
    public override void Do()
    {
    }
    
    public override void Undo()
    {
    }
  }
  
  public class AbundanceActivity : Activity
  {
    List<Chit.ElementType> validTypes;
    List<Chit> validChits;
    
    public AbundanceActivity(Chit.ElementType[] validTypes, Chit[] validChits) : this(new List<Chit.ElementType>(validTypes),
                                                                                      new List<Chit>(validChits))
    {
    }
    
    public AbundanceActivity(List<Chit.ElementType> validTypes, List<Chit> validChits)
    {
      this.validTypes = validTypes;
      this.validChits = validChits;
    }
    
    public override ActivityType Type {
      get { return ActivityType.Abundance; }
    }
    
    public override void Do()
    {
    }
    
    public override void Undo()
    {
    }
  }
}