﻿using BookStore.BLL.DTOs.User;
using BookStore.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserDto>>> Get()
    {
        return Ok(await _userService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserDto>> Create([FromBody] NewUserDto user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        return Ok(await _userService.Create(user));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UserDto user)
    {
        return Ok(await _userService.Update(user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return NoContent();
    }
}