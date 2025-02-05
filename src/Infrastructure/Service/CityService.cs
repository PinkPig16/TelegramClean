using Application.IRepository;
using Domain.Entities;


namespace Infrastructure.Service;
public class CityService
{
    private readonly ICityRepository _cityRepository;


    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;

    }
    public async Task<City> HandleCityAsync(List<string> words)
    {
        var city = await _cityRepository.GetAsyncByNameArray(words);

        return city;
    }
}

