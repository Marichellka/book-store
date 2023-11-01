using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Category;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class CategoryService: BaseService
{
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<CategoryDto>> GetAll(ISpecification<Category>? specification = null)
    {
        var categories = await UnitOfWork.CategoryRepository.GetAll(specification);
        return Mapper.Map<ICollection<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await UnitOfWork.CategoryRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Category), id);

        return Mapper.Map<CategoryDto>(category);
    }
    
    public async Task<CategoryDto> Create(NewCategoryDto category)
    {
        var categoryEntity = Mapper.Map<Category>(category);
        await UnitOfWork.CategoryRepository.Add(categoryEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<CategoryDto>(categoryEntity);
    }

    public async Task<CategoryDto> Update(CategoryDto updatedCategory)
    {
        if (await UnitOfWork.CategoryRepository.GetById(updatedCategory.Id) is null)
            throw new NotFoundException(nameof(Category), updatedCategory.Id);

        await UnitOfWork.CategoryRepository.Update(Mapper.Map<Category>(updatedCategory));
        await UnitOfWork.SaveChangesAsync();

        return updatedCategory;
    }

    public async Task Delete(int id)
    {
        var category = await UnitOfWork.CategoryRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Category), id);

        await UnitOfWork.CategoryRepository.Delete(category);
        await UnitOfWork.SaveChangesAsync();
    }
}