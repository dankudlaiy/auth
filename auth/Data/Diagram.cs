using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace auth.Data;

public class Diagram
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}