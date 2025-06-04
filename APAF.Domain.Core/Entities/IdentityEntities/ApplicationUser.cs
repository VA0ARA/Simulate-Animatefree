using Microsoft.AspNetCore.Identity;
using System;

namespace APAF.Domain.Core.Entities.IdentityEntities
{
 public class ApplicationUser : IdentityUser<Guid>
 {
  public string? PersonName { get; set; }
 }
}
