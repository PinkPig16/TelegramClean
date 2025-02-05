using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities;
public class Subscriptions
{
    public int Id { get; set; }
    [StringLength(100)]
    public string? Name { get; set; }
    [ForeignKey(nameof(AppUser))]
    public int AppUserId {  get; set; }
    public AppUser? AppUser { get; set; }
    public bool sub { get; set; } = false;
    [ForeignKey(nameof(City))]
    public int? CityId { get; set; }
    public City? City { get; set; }
    [StringLength(100)]
    public string? Url { get; set; }
    [StringLength(110)]
    public int CallBackId { get; set; }
    [StringLength(100)]
    public string? Keywords { get; set; }
    [StringLength(100)]
    public string? Location { get; set; }
    public DateTime? LastChecked { get; set; }
    public DateTime NextChek { get; set; }
    public ICollection<Vacancies>? vacancies { get; set; } = [];
}

