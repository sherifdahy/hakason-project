using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.Helpers;

public static class OtpHelper
{
    public static string Generate(int length = 6)
    {
        var bytes = new byte[4];
        System.Security.Cryptography.RandomNumberGenerator.Fill(bytes);
        var num = BitConverter.ToUInt32(bytes, 0) % (uint)Math.Pow(10, length);
        return num.ToString().PadLeft(length, '0');
    }

    public static string Hash(string code)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));
        return Convert.ToBase64String(hash);
    }

    public static bool Verify(string code, string hashed) => Hash(code) == hashed;
}