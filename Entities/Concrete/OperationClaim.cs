using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class OperationClaim : IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
