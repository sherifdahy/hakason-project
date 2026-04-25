namespace App.Domain.Abstraction.Consts;

public static class DefaultRoles
{
    public static partial class Admin
    {
        public const int Id = 1;
        public const string name = nameof(Admin);
        public const string ConcurrencyStamp = "0AC0FDBA-97EF-420E-BB14-F24C30DAF029";
    };

    public static partial class Client
    {
        public const int Id = 2;
        public const string name = nameof(Client);
        public const string ConcurrencyStamp = "BFE4CA72-A6AB-46C0-8713-51AD68AE3CEC";
    };
}
