using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class Product : IEntity 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? StockAmount { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }
}
