using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Ecom.Common;

namespace Ecom.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        [HttpPost]
        [Route("api/Products/AddProduct")]
        public MessageFor AddProduct(ProductItem data)
        {
            return Models.Products.AddProduct(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Products/GetProduct")]
        public DataTable GetProduct()
        {
            return Models.Products.GetProduct();
        }

        [HttpPost]
        [Route("api/Products/UpdateProduct")]
        public MessageFor UpdateProduct(ProductItem data)
        {
            return Models.Products.UpdateProduct(data);
        }
    }
}
