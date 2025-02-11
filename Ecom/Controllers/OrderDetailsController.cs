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
    public class OrderDetailsController : ApiController
    {
        [HttpPost]
        [Route("api/OrderDetails/AddOrder")]
        public MessageFor AddOrder(OrderDetail data)
        {
            return Models.OrderDetails.AddOrder(data);
        }

        [HttpPost]
        [Route("api/OrderDetails/UpdateOrder")]
        public MessageFor UpdateOrder(OrderDetail data)
        {
            return Models.OrderDetails.UpdateOrder(data);
        }

        [HttpPost]
        [Route("api/OrderDetails/DeleteOrder")]
        public MessageFor DeleteOrder(OrderDetail data)
        {
            return Models.OrderDetails.DeleteOrder(data);
        }

        [HttpPost]
        [Route("api/OrderDetails/GetOrderDetails")]
        public DataTable GetOrderDetails(OrderDetail data)
        {
            return Models.OrderDetails.GetOrderDetails(data);
        }
    }
}
