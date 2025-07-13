using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobber.Auth.Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; private set; }
    
    [Column("email")]
    [Required]
    public required string Email { get; set; }
    
    [Column("password_hash")]
    [Required]
    public required string PasswordHash { get; set; }
    
    [Column("is_email_confirmed")]
    [Required]
    public required bool IsEmailConfirmed { get; set; }
}