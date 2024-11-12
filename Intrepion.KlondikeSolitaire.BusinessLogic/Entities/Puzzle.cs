﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Puzzle
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Player? CreatorId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    // ActualPropertyPlaceholder
}