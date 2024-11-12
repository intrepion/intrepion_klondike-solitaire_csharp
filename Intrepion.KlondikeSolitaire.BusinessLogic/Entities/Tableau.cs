﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class Tableau
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Game? GameId { get; set; }
    public int PileIndex { get; set; }
    // ActualPropertyPlaceholder
}
