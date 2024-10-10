using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Gender
{
    public long Id { get; set; }

    public string? Descriptions { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
