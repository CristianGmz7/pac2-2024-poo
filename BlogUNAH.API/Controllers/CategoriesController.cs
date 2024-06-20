using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Services;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BlogUNAH.API.Controllers;

[ApiController]

// Route define todo lo que esta dentro del controlador con esta URL inicial
[Route("api/categories")]

// ApiController y ControllerBase siempre deben colocarse en los controladores
public class CategoriesController(ICategoriesService CategoriesService) : ControllerBase
{

    // Metodo constructor
    // Aqui se quito lo del metodo construcuto y se envio como parametros en la clase

    // las peticiones siempre deben ser publicas
    // Se esta mandando la url del parametro linea [Route("api/categories")]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await CategoriesService.GetCategoriesListAsync());
    }

    // URL diferente
    // El id se recibe como parametro de argumento y tambien como parametro dentro del metodo
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {

        //var category = _categories.Select(c => c.Id == id).FirstOrDefault()
        var category = await CategoriesService.GetCategoryByIdAsync(id);

        if(category == null)
        {
            return NotFound(new { Message = $"No se encontro la categoria: {id}" });
        }

        return Ok(category);
    }

    [HttpPost]
    // Siempre ira ActionResult para peticiones
    public async Task<ActionResult> Create (CategoryCreateDto dto)
    {
        //bool categoryExist = _categories
        //    .Any(c => c.Name.ToUpper().Trim().Contains(category.Name.ToUpper()));

        //if (!categoryExist)
        //{
        //    return BadRequest("La categoria ya esta registrada.");
        //}

        //category.Id = Guid.NewGuid();

        //_categories.Add(category);

        //return Created();

        bool flag = await CategoriesService.CreateAsync(dto);

        if (!flag) {
            return BadRequest("La categoria ya esta registrada.");
        }

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Edit(CategoryEditDto dto, Guid id)
    {
        var resultBool = await CategoriesService.EditAsync(dto, id);

        if(!resultBool)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete (Guid id)
    {
        var categoryResponse = await CategoriesService.GetCategoryByIdAsync(id);

        if (categoryResponse is null)
        {
            return NotFound();
        }

        await CategoriesService.DeleteAsync(id);

        return Ok();
    }
}
