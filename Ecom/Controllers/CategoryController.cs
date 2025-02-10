using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Ecom.Common;

namespace Ecom.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpPost]
        [Route("api/Category/Insert")]
        public  MessageFor CategoryInsert(Categories data)
        { 
            return Models.Category.InsertCategory(data); 
        }
        [HttpPost]
        [Route("api/Category/Add")]
        public MessageFor AddInsert(Categories data)
        {
            return Models.Category.AddCategory(data);

        }
        [HttpPost]
        [Route("api/Category/UpdateCategory")]
        public MessageFor UpdateCategory(Categories data)
        {
            return Models.Category.UpdateCategory(data);

        }
        [HttpPost]
        [Route("api/Category/DeleteCategory")]
        public MessageFor DeleteCategory(Categories data)
        {
            return Models.Category.DeleteCategory(data);

        }


    }
}
