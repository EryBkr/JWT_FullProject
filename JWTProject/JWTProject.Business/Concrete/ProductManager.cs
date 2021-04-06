using JWTProject.Business.Abstracts;
using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Concrete
{
   public class ProductManager:GenericManager<Product>,IProductService
    {
        public ProductManager(IGenericDal<Product> genericDal):base(genericDal)
        {

        }
    }
}
