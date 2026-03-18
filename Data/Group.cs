using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class Group
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public int? SpecialtyId { get; set; }

    public string? Description { get; set; }

    public virtual Specialty? Specialty { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
