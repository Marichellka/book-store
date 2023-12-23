﻿using AutoMapper;
using BookStore.BLL.DTOs.Category;
using BookStore.BLL.Exceptions;
using BookStore.BLL.MappingProfiles;
using BookStore.BLL.Services;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Books;
using BookStore.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookStore.Tests.Tests;

public class CategoryTests: TestBase
{
    private  CategoryService _categoryService;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _categoryService = new CategoryService(_unitOfWork, _mapper);
    }
    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _categoryService.GetAll();

        Assert.IsEmpty(returnedList);
    }

    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var category = CreateCategory("Category");

        var expectedList = new List<CategoryDto>()
        {
            _mapper.Map<CategoryDto>(category)
        };

        var returnedList = await _categoryService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public async Task GetAll_BooksCategories_ReturnedFilteredList()
    {
        var category1 = CreateCategory("Category1");
        CreateCategory("Category2");

        var book = CreateBook("Book", CreateAuthor("Author").Id, CreatePublisher("Publisher").Id);

        var joint = JoinBookCategory(book.Id, category1.Id);

        var expectedList = new List<CategoryDto>()
        {
            _mapper.Map<CategoryDto>(category1)
        };

        var returnedList = await _categoryService.GetAll(new BookCategoriesSpecification(book.Id));

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }

    [Test]
    public void GetById_NonExistingCategory_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _categoryService.GetById(1));
    }

    [Test]
    public async Task GetById_ExistingCategory_ReturnedCategory()
    {
        var category = CreateCategory("Category");

        var expectedCategory = _mapper.Map<CategoryDto>(category);

        var returnedCategory = await _categoryService.GetById(category.Id);

        Assert.That(returnedCategory, Is.EqualTo(expectedCategory));
    }

    [Test]
    public void Update_NonExistingCategory_ThrownNotFoundException()
    {
        CategoryDto categoryDto = new CategoryDto()
        {
            Id = 1,
            Name = "Category"
        };

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _categoryService.Update(categoryDto));
    }

    [Test]
    public async Task Update_ExistingCategory_CategoryUpdated()
    {
        var category = CreateCategory("Category");

        CategoryDto updatedCategory = new CategoryDto()
        {
            Id = category.Id,
            Name = "Category2"
        };

        var expectedCategory = new CategoryDto()
        {
            Id = category.Id,
            Name = "Category2",
        };

        var returnedCategory = await _categoryService.Update(updatedCategory);

        Assert.That(returnedCategory, Is.EqualTo(expectedCategory));
    }

    [Test]
    public void Delete_NonExistingCategory_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _categoryService.Delete(1));
    }

    [Test]
    public async Task Delete_ExistingCategory_CategoryDeleted()
    {
        var category = CreateCategory("Category");

        await _categoryService.Delete(category.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _categoryService.GetById(category.Id));
    }
}