using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductWithImagesDto
    {
        public Product Product { get; set; }
        public List<Productİmage> ProductImages { get; set; }
    }
}
