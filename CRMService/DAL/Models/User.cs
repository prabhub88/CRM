using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public long UserType { get; set; }

    public string Password { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime Created { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime Modified { get; set; }

    public virtual UserType UserTypeNavigation { get; set; } = null!;
}
