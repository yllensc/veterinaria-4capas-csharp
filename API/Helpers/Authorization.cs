namespace API.Helpers;

public class Authorization
{
    public enum Roles
    {
        Administrator,
        Veterinarian,
        WithoutRol
    }
    public const Roles rol_default = Roles.WithoutRol;
}
