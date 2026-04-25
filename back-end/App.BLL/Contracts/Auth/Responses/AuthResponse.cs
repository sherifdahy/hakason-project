namespace App.BLL.Contracts.Auth.Responses;

public record AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public int ExpireIn { get; set; }
}
