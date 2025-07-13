using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobber.Auth.Domain.Entities;

public class RefreshToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; private set; }
    [Column("user_id")]
    public required Guid UserId { get; set; }
    [Column("token")]
    public required string Token { get; set; }
    [Column("issued_at")]
    [Required]
    public required DateTimeOffset IssuedAt { get; set; }
    [Column("expires_at")]
    public required DateTimeOffset ExpiresAt { get; set; }
    [Column("is_revoked")]
    public required bool IsRevoked { get; set; } = false;
    
    public bool IsActive => !IsRevoked && ExpiresAt > DateTime.UtcNow;

}