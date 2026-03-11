using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class Login
{
    public int Id { get; set; }

    public string? Login1 { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
