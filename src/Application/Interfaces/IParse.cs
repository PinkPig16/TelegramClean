using Domain.Entities;

namespace Application.Interfaces;

public interface IParse
{
    Task JobCount(Subscriptions subscriptions);
    Task TestParse();
    Task GetVacancies();
}
