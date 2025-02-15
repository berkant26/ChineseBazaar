using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete;

public partial class Address :IEntity
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? AddressDescription { get; set; }

    public int? CityId { get; set; }

    public int? DistrictId { get; set; }

    public int? NeighborhoodId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
