using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class Productİmage : IEntity
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? İmageUrl { get; set; }
}
