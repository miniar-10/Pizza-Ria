using Backend.Models;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly ILogger<PizzaController> _logger;
    private readonly IPizzaService _service;

    public PizzaController(IPizzaService service, ILogger<PizzaController> logger)
    {
        _service = service;
        _logger = logger ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all pizzas at {Time}", DateTime.UtcNow);

        return Ok(await _service.getAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("Fetching pizza at {Time}", DateTime.UtcNow);

        var item = await _service.getByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        _logger.LogInformation("Creating new pizza at {Time}", DateTime.UtcNow);
        var created = await _service.AddAsync(pizza);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        _logger.LogInformation("Updating pizza at {Time}", DateTime.UtcNow);

        var success = await _service.UpdateAsync(id, pizza);
        return success ? Ok(success) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting pizza at {Time}", DateTime.UtcNow);

        var success = await _service.DeleteAsync(id);
        return success ? Ok(success) : NotFound();
    }
}
