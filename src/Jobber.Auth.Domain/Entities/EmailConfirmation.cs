using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobber.Auth.Domain.Entities;

[Table("email_confirmations")]
public class EmailConfirmation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; private set; }
    
    [Column("user_id")]
    [Required]
    public Guid UserId { get; set; }
    
    [Column("token")]
    [Required]
    public DateTimeOffset ExpiresAt { get; set; }
    
    [Column("is_used")]
    [Required]
    public bool IsUsed { get; set; }
}