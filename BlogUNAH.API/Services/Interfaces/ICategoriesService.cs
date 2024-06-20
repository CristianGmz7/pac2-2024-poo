using BlogUNAH.API.Dtos.Categories;

namespace BlogUNAH.API.Services.Interfaces;

public interface ICategoriesService
{
    // Se envuelve lo que retorna como una Task, haciendo referencia a asincronia
    // Esto funciona cuando hay problemas de base de datos u archivos muy grandes
    Task<List<CategoryDto>> GetCategoriesListAsync();

    Task<CategoryDto> GetCategoryByIdAsync(Guid id);

    Task<bool> CreateAsync(CategoryCreateDto dto);

    Task<bool> EditAsync(CategoryEditDto dto, Guid id);

    Task<bool> DeleteAsync(Guid id);

}

