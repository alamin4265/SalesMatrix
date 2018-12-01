using SalesMatrix.Entity.Models;
using SalesMatrix.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace SalesMatrix.WebAPI.Controllers
{
    
    public class RoleController : ApiController
    {
        private RoleInfoService _roleService = new RoleInfoService();

        [HttpPost]
        [Route("api/role")]
        public void Create(Role role)
        {
            _roleService.Create(role);
        }

        [HttpPut]
        [Route("api/role")]
        public void Update(Role role)
        {
            _roleService.Update(role);
        }

        [HttpDelete]
        [Route("api/role/{id}")]
        public void Delete(int id)
        {
            _roleService.Delete(id);
        }

        [HttpGet]
        [Route("api/roles")]
        public IHttpActionResult GetAll()
        {
            var RoleList = _roleService.GetAll();
            return Json(RoleList);
        }

        [HttpGet]
        [Route("api/activeRoles")]
        public IHttpActionResult GetAllActiveRole()
        {
            var RoleList = _roleService.GetAllActiveRole();
            return Json(RoleList);
        }

        [HttpGet]
        [Route("api/inactiveRoles")]
        public IHttpActionResult GetAllInactiveRole()
        {
            var RoleList = _roleService.GetAllInactiveRole();   
            return Json(RoleList);
        }

        [HttpGet]
        [Route("api/role/{id}")]
        public IHttpActionResult GetById(int id)
        {
            Role role = _roleService.GetById(id);
            if(role==null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet]
        [Route("api/RoleExists/{name}")]
        public JsonResult<bool> RoleExists(string name) 
        {
            return Json(_roleService.RoleExists(name));
        }

        [HttpGet]
        [Route("api/RoleExistsForEdit/{name}/{id}")]
        public JsonResult<bool> RoleExistsForEdit(string name,int id)
        {
            return Json(_roleService.RoleExistsForEdit(name,id));
        }
    }
}
