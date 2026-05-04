namespace App.Domain.Abstraction.Consts;

public static class DefaultRoles
{
    public static partial class Admin
    {
        public const int Id = 1;
        public const string Name = nameof(Admin);
        public const string ConcurrencyStamp = "0AC0FDBA-97EF-420E-BB14-F24C30DAF029";
    };

    public static partial class Parent
    {
        public const int Id = 2;
        public const string Name = nameof(Parent);
        public const string ConcurrencyStamp = "BFE4CA72-A6AB-46C0-8713-51AD68AE3CEC";
    };

    public static partial class Child
    {
        public const int Id = 3;
        public const string Name = nameof(Child);
        public const string ConcurrencyStamp = "01C89763-4DC4-43B4-948B-E1D792271DA1";
    };
}
