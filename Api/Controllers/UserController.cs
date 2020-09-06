using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<User> service;

        public UserController(IService<User> userService)
        {
            this.service = userService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return service.Get().Data;
        }
    }
}