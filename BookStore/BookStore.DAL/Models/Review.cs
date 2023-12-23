﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class Review: BaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    [Range(1, 5)]
    public float Rating { get; set; }
    public string? TextReview { get; set; }
}