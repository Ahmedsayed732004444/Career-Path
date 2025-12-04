namespace Career_Path.Abstractions.Consts;

public static class Permissions
{
    public static string Type { get; } = "permissions";
       //Permitions 
    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
}