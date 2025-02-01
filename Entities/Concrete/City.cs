using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class City : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
