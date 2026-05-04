namespace App.Domain.Contracts.Auth.Responses;

public record AuthResponse(int Id, string Name, string Role, string Token, int ExpireIn);
