using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Services.Interfaces;
using Newtonsoft.Json;

namespace BlogUNAH.API.Services;

// ctrl + punto en interfaz para implementar las interfaces
public class CategoriesService : ICategoriesService
{

    // Con readonly se crea un archivo constante que no se cambia
    public readonly string _JSON_FILE;

    public CategoriesService()      // Metodo Constructor
    {
        _JSON_FILE = "SeedData/categories.json";
    }

    public async Task<List<CategoryDto>> GetCategoriesListAsync()
    {
        return await ReadCategoriesFromFilesAsync();
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        var categories = await ReadCategoriesFromFilesAsync();
        return categories.FirstOrDefault(c => c.Id == id);
        //var categories = await ReadCategoriesFromFilesAsync();
        //CategoryDto category = categories.FirstOrDefault(c => c.Id == id);
        //return category;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto)
    {
        var categoriesDtos = await ReadCategoriesFromFilesAsync();

        bool flag = await CheckCategory(dto);

        if (!flag)
        {
            return false;
        }

        var categoryDto = new CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
        };

        categoriesDtos.Add(categoryDto);

        //pasar de Category Dto a Category Entity
        //Esto es debido a que el metodo Write debe recibir lista como una entidad y no como
        //un dto
        var categories = categoriesDtos.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToList();

        ////Otra manera de pasar Category Dto a Category Entity
        //var categoriesList = new List<Category>();

        //for (int i = 0; i < categoriesList.Count; i++)
        //{
        //    var cat = new Category
        //    {
        //        Id = categoriesList[i].Id,
        //        Name = categoriesList[i].Name,
        //        Description = categoriesList[i].Description,
        //    };
        //    categories.Add(cat);
        //}

        await WriteCategoriesToFileAsync(categories);

        return true;
    }

    public async Task<bool> EditAsync(CategoryEditDto dto, Guid id)
    {
        var categoriesDto = await ReadCategoriesFromFilesAsync();

        var existingCategory = categoriesDto.
            FirstOrDefault(category => category.Id == id);

        if (existingCategory is null)
        {
            return false;
        }

        for (int i = 0; i < categoriesDto.Count; i++)
        {
            if (categoriesDto[i].Id == id)
            {
                categoriesDto[i].Name = dto.Name;
                categoriesDto[i].Description = dto.Description;
            }
        }

        //pasar de Category Dto a Category Entity
        //Explicacion breve: convertir el dto que el usuario ingreso a una entidad que 
        //necesita la base de datos
        var categories = categoriesDto.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToList();


        await WriteCategoriesToFileAsync(categories);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var categoriesDto = await ReadCategoriesFromFilesAsync();
        var categoryToDelete = categoriesDto.FirstOrDefault(c => c.Id == id);

        if(categoryToDelete is null)
        {
            return false;
        }

        categoriesDto.Remove(categoryToDelete);

        //pasar de Category Dto a Category Entity
        var categories = categoriesDto.Select(c => new Category
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToList();

        await WriteCategoriesToFileAsync(categories);

        return true;
    }

    private async Task<List<CategoryDto>> ReadCategoriesFromFilesAsync()
    {
        if (!File.Exists(_JSON_FILE)){
            return new List<CategoryDto>();
        }

        var json = await File.ReadAllTextAsync(_JSON_FILE);

        //Los archivos en la base de datos se guardan como la clase que se encuentra la
        //entidad, en este caso la entidad es Category
        var categories = JsonConvert.DeserializeObject<List<Category>>(json);

        //Convertir Entity (lo que esta en base de datos) a Dto (lo que necesita el cliente)
        //Al final se pone .ToList() porque el metodo .Select retorna un IEnumerable y no una lista
        var dtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToList();

        return dtos;
    }

    //El metodo Write no retorna nada pero debe escribir los archivos que se van a escribir
    //en la base de datos como la entidad y no como el dto
    private async Task WriteCategoriesToFileAsync(List<Category> categories)
    {
        var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

        if (File.Exists(_JSON_FILE))
        {
            await File.WriteAllTextAsync(_JSON_FILE, json);
        }
    }

    //Metodo para comprobar si se repite el nombre de una categoria o no
    private async Task<bool> CheckCategory(CategoryCreateDto dto)
    {

        var categories = await ReadCategoriesFromFilesAsync();

        for (int i = 0; i < categories.Count; i++)
        {
            if(dto.Name.ToUpper().Trim() == categories[i].Name.ToUpper().Trim())
            {
                return false;
            }
        }

        return true;
    } 

}
