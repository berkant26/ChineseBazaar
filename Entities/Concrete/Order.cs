using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class Order : IEntity
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? UserAddressId { get; set; }

    public bool? PaymentStatus { get; set; }
}
