using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class Neighborhood : IEntity
{
    public int Id { get; set; }

    public int DistrictId { get; set; }

    public string Name { get; set; } = null!;
}
