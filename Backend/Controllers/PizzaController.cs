using Backend.Models;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _service;

   public PizzaController(IPizzaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()=>
        Ok(await _service.getAllAsync());

    [HttpGet("{id}")] public async Task<IActionResult> Get(int id)
    {
        var item=await _service.getByIdAsync(id);
        return item ==null?NotFound(): Ok(item); 
    }
    [HttpPost]
    public async Task<IActionResult> Create( Pizza pizza)
    {
        var created = await _service.AddAsync(pizza);
        return CreatedAtAction(nameof(Get), new {id=created.Id},created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        var success = await _service.UpdateAsync(id, pizza);
        return success ? Ok(success) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task <IActionResult> Delete (int id)
    {
        var success= await _service.DeleteAsync(id);
        return success ? Ok(success) : NotFound();
    }
}
