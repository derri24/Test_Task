using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;

public class OrderService : IOrderService
{
    private ApplicationDbContext _applicationDbContext;

    public OrderService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Order> GetOrder()
    {
        var order = _applicationDbContext.Orders
            .Include(x=>x.User)
            .OrderByDescending(x => x.Price).First();
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _applicationDbContext.Orders
            .Include(x=>x.User)
            .Where(x => x.Quantity > 10).ToListAsync();
        return orders;
    }
}