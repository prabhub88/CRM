using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class UserType
{
    public long Id { get; set; }

    public string UserType1 { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public bool? IsActive { get; set; }
}
