using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Ecom.Common;

namespace Ecom.Controllers
{
    public class OrderDetailsController : ApiController
    {
        [HttpPost]
        [Route("api/OrderDetails/AddOrder")]
        public MessageFor AddOrder(OrderDetail data)
        {
            return Models.OrderDetails.AddOrder(data);
        }
    }
}
