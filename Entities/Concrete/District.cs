using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class District : IEntity
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
