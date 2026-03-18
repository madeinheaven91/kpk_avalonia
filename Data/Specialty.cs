using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class Specialty
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
