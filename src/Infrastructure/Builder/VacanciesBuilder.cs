using Domain.Entities;

namespace Infrastructure.Builder;

public class VacanciesBuilder
{
    private readonly Vacancies _vacancies;
    
    public VacanciesBuilder(Vacancies vacancies) => _vacancies = vacancies;

    public VacanciesBuilder SetUrl(string url)
    {
        _vacancies.URL = url;
        return this;
    }
    public VacanciesBuilder SetName(string siteName)
    {
        _vacancies.SiteName = siteName;
        return this;
    }

    public VacanciesBuilder SetId(int id)
    {
        _vacancies.Id = id;
        return this;
    }
    public VacanciesBuilder SetTitle(string title)
    {
        _vacancies.Title = title;
        return this;
    }
    public VacanciesBuilder SetSalary(string salary)
    {
        _vacancies.Salary = salary;
        return this;
    }
    public VacanciesBuilder SetDescription(string description)
    {
        _vacancies.Description = description;
        return this;
    }
    public VacanciesBuilder SetCity(City? city)
    {
        _vacancies.City = city;
        return this;
    }
    public VacanciesBuilder SetAppUser(AppUser appUser)
    {
        _vacancies.AppUser = appUser;
        return this;
    }

    public Vacancies Build()
    {
        return _vacancies;
    }
}