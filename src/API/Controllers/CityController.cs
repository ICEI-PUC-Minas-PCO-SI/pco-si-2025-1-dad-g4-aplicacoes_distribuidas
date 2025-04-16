using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public CityController(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    // GET: NotificationController
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        await _dbContext.Cities.AddAsync(new City
        {
            Name = "São Paulo",
            CountryId = 5
        });

        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Cities.ToListAsync();
        return Ok(result);
    }

}
