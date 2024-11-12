﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationNamePlaceholder.BusinessLogic.Entities;

public class CardWaste
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }
    public Guid Id { get; set; }

    public Card? CardId { get; set; }
    public Game? GameId { get; set; }
    // ActualPropertyPlaceholder
}
