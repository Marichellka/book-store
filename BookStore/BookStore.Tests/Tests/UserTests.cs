using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BookStore.BLL.DTOs.User;
using BookStore.BLL.Exceptions;
using BookStore.BLL.Jwt;
using BookStore.BLL.MappingProfiles;
using BookStore.BLL.Services;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookStore.Tests.Tests;

public class UserTests: TestBase
{
    private  UserService _userService;
    private JwtGenerator _jwtGenerator;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        
        var jwtConfiguration = new ConfigurationMock(new()
        {
            {"JwtSettings:Key", "mock_key_afkkfbckue46hwwahpd61hjfwq8uhwkjasfbkq22nflasifowqi834"},
            {"JwtSettings:Issuer", "https://localhost:mock"},
            {"JwtSettings:Audience", "https://localhost:mock"}
        });
        _jwtGenerator = new JwtGenerator(jwtConfiguration);
        
        _userService = new UserService(_unitOfWork, _mapper, _jwtGenerator);
    }
    
    [Test]
    public async Task GetAll_EmptyDB_ReturnedEmptyList()
    {
        var returnedList = await _userService.GetAll();

        Assert.IsEmpty(returnedList);
    }

    [Test]
    public async Task GetAll_NotEmptyDB_ReturnedList()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        var expectedList = new List<UserDto>()
        {
            _mapper.Map<UserDto>(user)
        };

        var returnedList = await _userService.GetAll();

        Assert.That(returnedList, Is.EqualTo(expectedList));
    }
    
    [Test]
    public void GetById_UserDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _userService.GetById(1));
    }

    [Test]
    public async Task GetById_UserExists_ReturnedUser()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        var expectedUser = _mapper.Map<UserDto>(user);

        var returnedUser = await _userService.GetById(user.Id);

        Assert.That(returnedUser, Is.EqualTo(expectedUser));
    }
    
    [Test]
    public void Login_UserDoesNotExist_ThrownNotFoundException()
    {
        var user = new NewUserDto()
        {
            Name = "User"
        };
        
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _userService.Login(user));
    }
    
    [Test]
    public async Task Login_UserExists_ReturnedToken()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        var loginUser = new NewUserDto()
        {
            Name = "User",
            Password = "Password",
            Email = "some@mail.com"
        };
        
        var jwt = await _userService.Login(loginUser);

        AssertJwtCorrect(jwt, user);
    }

    private void AssertJwtCorrect(string jwt, User user)
    {
        JwtSecurityTokenHandler handler = new();
        var decodedToken = (JwtSecurityToken)handler.ReadToken(jwt);

        string id = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string name = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        Assert.That(id, Is.EqualTo(user.Id.ToString()));
        Assert.That(name, Is.EqualTo(user.Name));
    }


    [Test]
    public void Update_UserDoesNotExist_ThrownNotFoundException()
    {
        UserDto userDto = new UserDto()
        {
            Id = 1,
            Name = "User"
        };

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _userService.Update(userDto));
    }

    [Test]
    public async Task Update_UserExists_UserUpdated()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        UserDto updatedUser = new UserDto()
        {
            Id = user.Id,
            Name = "User2",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password2"),
            Email = "some@mail.com"
        };

        var expectedUser = new UserDto()
        {
            Id = user.Id,
            Name = "User2",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password2"),
            Email = "some@mail.com"
        };

        var returnedUser = await _userService.Update(updatedUser);

        Assert.That(returnedUser, Is.EqualTo(expectedUser));
    }
    
    [Test]
    public void Delete_UserDoesNotExist_ThrownNotFoundException()
    {
        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _userService.Delete(1));
    }

    [Test]
    public async Task Delete_UserExists_UserDeleted()
    {
        var user = CreateUser("User", "some@mail.com", "Password");

        await _userService.Delete(user.Id);

        Assert.ThrowsAsync(typeof(NotFoundException), async Task() => await _userService.GetById(user.Id));
    }
}