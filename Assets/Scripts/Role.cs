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
    public int RoleID = 0;
    public string RoleName = "Placeholder";
    public Branch RoleBranch = Branch.None;
}
