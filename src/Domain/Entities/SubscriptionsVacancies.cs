using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SubscriptionsVacancies
{
    public int SubscriptionsId { get; set; }
    public Subscriptions Subscription { get; set; }
    
    public int VacanciesId { get; set; }
    public Vacancies Vacancy { get; set; }

}