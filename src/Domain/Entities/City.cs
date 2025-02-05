using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class City
{
    public int Id { get; set; }

    [StringLength(100)]
    [Required]
    public string Name { get; set; }

    [StringLength(100)]
    public string? NameEng { get; set; } 
        
}

