using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Intrepion.KlondikeSolitaire.BusinessLogic.Entities;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public ApplicationUser? ApplicationUserUpdatedBy { get; set; }

    // ActualPropertyPlaceholder
}
