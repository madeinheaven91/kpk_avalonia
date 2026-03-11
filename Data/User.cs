using System;
using System.Collections.Generic;

namespace kpk_avalonia.Data;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public DateOnly? Dob { get; set; }

    public int? LoginId { get; set; }

    public int? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Login? Login { get; set; }
}
