using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobber.Auth.Domain.Entities;

public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    
    [Column("is_email_confirmed")]
    public bool IsEmailConfirmed { get; set; }
}