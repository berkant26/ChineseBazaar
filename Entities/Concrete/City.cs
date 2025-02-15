using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class City : IEntity
{
    public string? Name { get; set; }

    public int Id { get; set; }
}
