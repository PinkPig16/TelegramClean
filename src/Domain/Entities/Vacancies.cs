using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Vacancies
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public required string SiteName { get; set; }
    [StringLength(100)]
    [Required]
    public required string URL { get; set; }
    [StringLength(100)]
    [Required]
    public string? Title { get; set; }
    [StringLength(100)]
    [Required]
    public string? Description { get; set; }
    public int? Salary { get; set; }
    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public City? City { get; set; }
    [ForeignKey(nameof(AppUser))]
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public DateTime DatePublished { get; set; }
    public ICollection<Subscriptions>? subscriptions { get; set; } = [];

}

