using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Ecom.Common;

namespace Ecom.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpPost]
        [Route("api/Orders/Insert")]
        public MessageFor Insert(Order data)
        {
            return Models.Orders.OrderInsert(data);
        }
    }
}
