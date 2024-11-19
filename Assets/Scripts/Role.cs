using System.Collections.Generic;

public enum Branch
{
    None = 0,
    Management = 1,
    Operations = 2,
    Marketing = 3,
    Technology = 4,
    Finance = 5,
}

public class Role {
    public int ID { get; private set; }
    public string Name { get; private set; }
    public Branch Branch { get; private set; }
    public List<int> ChallengeIDs { get; private set; }
    
    // public 
}
