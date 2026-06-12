using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class Group
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public int SpecialtyId { get; set; }

    public string? Description { get; set; }

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

	public override string ToString() => Number;
}
