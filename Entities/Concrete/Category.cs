using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Category : IEntity
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }
}
