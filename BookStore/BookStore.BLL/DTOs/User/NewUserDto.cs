﻿namespace BookStore.BLL.DTOs.User;

public class NewUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } 
}