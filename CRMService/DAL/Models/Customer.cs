using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Customer
{
    public long Id { get; set; }

    public long CustomerNumber { get; set; }

    public string? CustomerName { get; set; }

    public DateOnly? Dob { get; set; }

    public long? Gender { get; set; }

    public virtual Gender? GenderNavigation { get; set; }
}
