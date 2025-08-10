using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dto;

public record LoginUserRequestDto
{
    [Required, EmailAddress]
    [JsonPropertyName("email")]
    public required string Email { get; init; }
    [Required]
    [JsonPropertyName("password")]
    public required string Password { get; init; }
}