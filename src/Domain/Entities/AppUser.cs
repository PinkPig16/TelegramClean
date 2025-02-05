using System.ComponentModel.DataAnnotations;
using Telegram.Bot.Types;

namespace Domain.Entities;
public class AppUser : User
{
    [Key]
    public int dbId {get;set;}
    public ICollection<Subscriptions>? vacancies { get; set; } = [];
}



