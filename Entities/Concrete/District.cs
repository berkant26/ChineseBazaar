using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class District : IEntity
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public string Name { get; set; } = null!;
}
