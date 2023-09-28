using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;


//2. Возвращать пользователей с статусом Inaсtive
public class UserService:IUserService
{
    private ApplicationDbContext _applicationDbContext;

    public UserService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<User> GetUser()
    {
        var user = _applicationDbContext.Users
            .Include(x => x.Orders)
            .OrderByDescending(x => x.Orders.Count).First();
        return user;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = _applicationDbContext.Users
            .Include(x => x.Orders)
            .Where(x => x.Status == UserStatus.Inactive).ToList();
        return users;
    }
}