using Application.IRepository;
using AutoMapper;
using Domain.Entities;
using Telegram.Bot.Types;



namespace Infrastructure.Service;

public class AppUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IMapper _mapper;

    public AppUserService(IAppUserRepository appUserRepository, IMapper mapper)
    {
        _appUserRepository = appUserRepository;
        _mapper = mapper;   
    }
    public async Task<AppUser> HandleUserAsync(User user)
    {
        var existingUser = await _appUserRepository.GetAsyncById(user.Id);

        if (existingUser == null)
        { 
            var appUser = _mapper.Map<AppUser>(user);
            await _appUserRepository.Add(appUser);
            return appUser;
        }
        return existingUser;

    }
    public async Task UpdateAppUserAsync(AppUser appUser)
    {
        await _appUserRepository.UpdateAsync(appUser);
    }

    public void setVacancies(AppUser appUser, Subscriptions vacancies)
    {
        appUser.vacancies.Add(vacancies);
    }

    public async Task DeleteAppUser(long id)
    {
        var appUser = await _appUserRepository.GetAsyncById(id);
        if (appUser != null)
        {
            await _appUserRepository.DeleteAsync(appUser);
        }
    }
    public async Task AddAppUser(User user)
    {
        var existingUser = await _appUserRepository.GetAsyncById(user.Id);
    }
}

