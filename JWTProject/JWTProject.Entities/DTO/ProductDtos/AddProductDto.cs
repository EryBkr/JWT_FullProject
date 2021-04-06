using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Entities.DTO.ProductDtos
{
   public class AddProductDto:IDto
    {
        public string Name { get; set; }
    }
}
