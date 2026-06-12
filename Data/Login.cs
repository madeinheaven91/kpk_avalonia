using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class Login
{
    public int Id { get; set; }

    public string Login1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
