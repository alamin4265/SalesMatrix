using SalesMatrix.Entity.Models;
using SalesMatrix.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesMatrix.WebAPI.Controllers
{
    public class ResourceController : ApiController
    {
        private ResourceService _resourceService = new ResourceService();

        [HttpPost]
        [Route("api/resource")]
        public void Create(Resource resource)
        {
            _resourceService.Create(resource);
        }
    }
}
