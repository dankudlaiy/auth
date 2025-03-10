using System.ComponentModel.DataAnnotations;

namespace web.Data;

public class User
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Username { get; set; }
    public bool IsAdmin { get; set; }
}